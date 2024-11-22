using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.PersonAdministration.Interface.REST.Resources;

namespace KingReserveBack.PersonAdministration.Interface.REST.Transform;

public static class PersonResourceFromEntityAssembler
{
    public static PersonResource ToResourceFromEntity(Person entity)
    {
        return new PersonResource(
            entity.Id,
            entity.Name,
            entity.Age,
            entity.Date,
            entity.Country,
            entity.City,
            entity.District,
            entity.Observations,
            entity.RoomId);
    }
}