namespace Domain.Models.Authtification
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public virtual ICollection<UserAuth>? UserAuthes { get; set; }
        public virtual ICollection<Permission>? Permissions { get; set; }
    }
}
