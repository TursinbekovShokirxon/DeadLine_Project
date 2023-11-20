using Application.InterfacesModelServices;
using Domain.Models;
using Domain.Models.Authtification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clases
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(UserAuth user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiryInMinutes = Convert.ToDouble(10);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expired = DateTime.Now.AddMinutes(15),
            };
            return refreshToken;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public byte[] GenerateSalt()
        {
            byte[] salt = new byte[16]; 
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
        public string HashPassword(string password, byte[] salt)
        {
            using (var sha256 = new SHA256Managed())
            {
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
