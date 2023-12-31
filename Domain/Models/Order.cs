﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime dateCreated { get; set; }=DateTime.Now;
        public string Price { get; set; } = string.Empty;

        public int TaskId { get; set; }
        public Task? Task { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
