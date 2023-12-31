﻿using Application.InterfacesModelServices;
using Application.ModelServices;
using Domain.Models.Authtification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        public UserRegirstrationHandler(IUserAuthService _service, ITokenServices _tokenService) {
            this._tokenService = _tokenService;
            this._service = _service;
        }

        
        public async Task<string> Handle(UserRegirstrationModel request, CancellationToken cancellationToken)
        {
            UserAuth userInputInDB = new();
            string Hashpassword =_tokenService.HashPassword(request.Password);
            userInputInDB.Username = request.Username;
            userInputInDB.PasswordHash= Hashpassword;
            var userOutputInDB = await _service.Create(userInputInDB);
            if (userOutputInDB == null) return "Вы уже зарегистрированы в системе";
            return "Регистрация прошла успешно";

        }

    }
}
