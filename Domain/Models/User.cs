using Domain.State;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string Universty { get; set; } = string.Empty;
        public Course Course { get; set; }
        public string Faculty { get; set; } = string.Empty;
        public string Budget { get; set; } = string.Empty;

        public ICollection<Task>? Tasks { get; set; }

    }
}
