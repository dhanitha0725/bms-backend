using bms.Domain.Entities;

namespace bms.Application.Abstractions.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateJwtToken(User user);
    }
}
