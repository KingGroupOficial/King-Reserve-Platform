using KingReserveBack.IAM.Domain.Model.Aggregates;

namespace KingReserveBack.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> ValidateToken(string token);
}