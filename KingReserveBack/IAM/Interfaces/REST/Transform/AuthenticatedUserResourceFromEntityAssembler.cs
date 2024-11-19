using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.IAM.Interfaces.REST.Resources;

namespace KingReserveBack.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User entity, string token)
    {
        return new AuthenticatedUserResource(entity.Id, entity.Username, token);
    } 
}