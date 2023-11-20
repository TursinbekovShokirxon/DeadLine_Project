using Domain.Models.Authtification;

namespace Application.InterfacesModelServices
{
    public interface ITokenServices
    {
        public string GenerateToken(UserAuth user);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public byte[] GenerateSalt();
        public string HashPassword(string password, byte[] salt);
    }
}