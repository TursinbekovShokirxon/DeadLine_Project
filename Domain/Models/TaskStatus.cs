using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class TaskStatus
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }=string.Empty;
    }
}
