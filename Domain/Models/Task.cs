using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Descryption { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }

        public int TaskStatusId { get; set; }
        public TaskStatus? TaskStatus { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }


    }
}
