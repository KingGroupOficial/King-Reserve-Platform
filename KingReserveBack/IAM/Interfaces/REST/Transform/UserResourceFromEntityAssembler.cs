using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.IAM.Interfaces.REST.Resources;

namespace KingReserveBack.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
    {
        return new UserResource(entity.Id, entity.Username);
    }
}