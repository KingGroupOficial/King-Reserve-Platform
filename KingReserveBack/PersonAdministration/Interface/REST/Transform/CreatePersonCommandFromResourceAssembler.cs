using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Interface.REST.Resources;

namespace KingReserveBack.PersonAdministration.Interface.REST.Transform;

public static class CreatePersonCommandFromResourceAssembler
{
    public static CreatePersonCommand ToCommandFromResource(
        CreatePersonResource resource)
    {
        return new CreatePersonCommand(
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