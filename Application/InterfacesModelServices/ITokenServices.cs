using Domain.Models.Authtification;

namespace Application.InterfacesModelServices
{
    public interface ITokenServices
    {
        string GenerateToken(UserAuth user);
        public RefreshToken GenerateRefreshToken();
        public string HashPassword(string password);
        public bool VerifyPassword(string enteredPassword, string hashedPassword);
    }
}