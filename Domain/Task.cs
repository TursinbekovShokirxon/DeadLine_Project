using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string  Name { get; set; } = string.Empty;
        public string Descryption { get; set; } = string.Empty;
        public DateOnly Deadline { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
