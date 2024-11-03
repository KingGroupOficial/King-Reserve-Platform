using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public static class RoomResourceFromEntityAssembler
{
    public static RoomResource ToResourceFromEntity(Room entity)
    {
        return new RoomResource(
            entity.Id,
            entity.Name,
            entity.Area,
            entity.Status.ToString(),
            entity.ReserveId);
    }
}