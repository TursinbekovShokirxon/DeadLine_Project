﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Authtification
{
    public class UserAuth
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; }= string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }= DateTime.Now.ToUniversalTime();
        public DateTime TokenExpires { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
