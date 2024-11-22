using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Interface.REST.Resources;

namespace KingReserveBack.PersonAdministration.Interface.REST.Transform;

public static class UpdatePersonCommandFromResourceAssembler
{
    public static UpdatePersonCommand ToCommandFromResource(
        int personId,
        CreatePersonResource resource)
    {
        return new UpdatePersonCommand(
            personId,
            resource.Name,
            resource.Age,
            resource.Date,
            resource.Country,
            resource.City,
            resource.District,
            resource.Observations,
            resource.RoomId);
    }
}