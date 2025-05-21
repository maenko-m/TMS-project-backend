using TmsSolution.Domain.Entities;

namespace TmsSolution.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime ExpiresAt) GenerateToken(User user);
    }
}
