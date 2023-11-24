using Application.InterfacesModelServices;
using Application.ModelServices;
using Domain.Models;
using Domain.Models.Authtification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForAuthentication
{
    public class UserRegirstrationModel : IRequest<string>
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
    public class UserRegirstrationHandler : IRequestHandler<UserRegirstrationModel, string>
    {
        private readonly IUserAuthService _service;
        private readonly ITokenServices _tokenService;
        private readonly IUserService _userservice;

        public UserRegirstrationHandler(IUserAuthService _service, ITokenServices _tokenService, IUserService userservice) {
            this._tokenService = _tokenService;
            this._service = _service;
            _userservice = userservice;
        }

        
        public async Task<string> Handle(UserRegirstrationModel request, CancellationToken cancellationToken)
        {
            UserAuth userInputInDB = new();
            string Hashpassword =_tokenService.HashPassword(request.Password);
            userInputInDB.Username = request.Username;
            userInputInDB.PasswordHash= Hashpassword;
            UserAuth userOutputInDB = await _service.Create(userInputInDB);
        
            if (userOutputInDB == null) return "Вы уже зарегистрированы в системе";

            Domain.Models.User user = new()
            {
                Id = userOutputInDB.UserId
            };
            await _userservice.Create(user);
            return "Регистрация прошла успешно";

        }

    }
}
