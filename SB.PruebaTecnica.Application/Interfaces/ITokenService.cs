using SB.PruebaTecnica.Domain.Entities;

namespace SB.PruebaTecnica.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
