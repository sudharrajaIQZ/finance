using backend.Models;

namespace backend.Interface
{
    public interface IJwtInterface
    {
        string GenerateAccessToken(AuthModel user);
        string GenerateRefreshToken();
        (string AccessToken, string RefreshToken) GenerateToken(AuthModel user);
    }
}
