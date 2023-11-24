namespace Domain.Models.Authtification
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public ICollection<User> UserAuthes { get; set; }
    }
}
