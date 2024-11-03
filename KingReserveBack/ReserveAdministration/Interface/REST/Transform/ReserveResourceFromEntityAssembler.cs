using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public static class ReserveResourceFromEntityAssembler
{
    public static ReserveResource ToResourceFromEntity(Reserve entity)
    {
        return new ReserveResource(
            entity.Id, 
            entity.Name, 
            entity.DateStart, 
            entity.DateEnd, 
            entity.Condition.GetDisplayName(),
            entity.Duration,
            entity.UserId.Identifier);
    }
}