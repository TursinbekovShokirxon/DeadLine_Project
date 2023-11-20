using Application.ModelServices;
using Domain.Models.Authtification;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Handlers.ForAuthentication
{
    public class UserRegirstrationModel : IRequest<UserAuth>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserRegirstrationHandler : IRequestHandler<UserRegirstrationModel, UserAuth>
    {
        private readonly IUserAuthService _service;
        public UserRegirstrationHandler(IUserAuthService _service) =>
            this._service = _service;
        public async Task<UserAuth> Handle(UserRegirstrationModel request, CancellationToken cancellationToken)
        {
            UserAuth userInputInDB = new();
            CreatePasswordHash(request.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            userInputInDB.Username = request.Username;
            userInputInDB.PasswordHash = PasswordHash;
            userInputInDB.PasswordSalt = PasswordSalt;
            var userOutputInDB = await _service.Create(userInputInDB);
            return userOutputInDB;

        }
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
