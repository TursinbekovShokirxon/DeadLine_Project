using Domain.Models.Authtification;

namespace Application.InterfacesModelServices
{
    public interface ITokenServices
    {
        string GenerateToken(UserAuth user);
        public RefreshToken GenerateRefreshToken();
    }
}