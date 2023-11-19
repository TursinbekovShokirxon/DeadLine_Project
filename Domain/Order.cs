namespace Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime dategiven { get; set; }
        public string Price { get; set; } = string.Empty;
        public int TaskId { get; set; }    
        public Domain.Task Task { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
