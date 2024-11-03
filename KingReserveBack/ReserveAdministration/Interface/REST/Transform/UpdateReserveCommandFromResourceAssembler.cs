using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public class UpdateReserveCommandFromResourceAssembler
{
    public static UpdateReserveCommand ToCommandFromResource(
        int reserveId,
        CreateReserveResource resource)
    {
        return new UpdateReserveCommand(
            reserveId, 
            resource.Name,
            resource.DateStart,
            resource.DateEnd);
    }
}