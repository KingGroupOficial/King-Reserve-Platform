using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public static class CreateReserveCommandFromResourceAssembler
{
    public static CreateReserveCommand ToCommandFromResource(
        CreateReserveResource resource)
    {
        return new CreateReserveCommand(
            resource.Name, 
            resource.DateStart, 
            resource.DateEnd,
            resource.userId);
        
    }
}