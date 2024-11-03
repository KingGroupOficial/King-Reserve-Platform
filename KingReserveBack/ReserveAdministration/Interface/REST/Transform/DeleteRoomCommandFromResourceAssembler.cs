using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public static class DeleteRoomCommandFromResourceAssembler
{
    public static DeleteRoomToReserveCommand ToCommandFromResource(
        int reserveId, DeleteRoomResource resource)
    {
        return new DeleteRoomToReserveCommand(reserveId,
            resource.roomId);
    }
}