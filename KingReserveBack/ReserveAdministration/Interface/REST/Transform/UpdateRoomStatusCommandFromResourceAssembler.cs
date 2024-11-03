using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public class UpdateRoomStatusCommandFromResourceAssembler
{
    public static UpdateStatusRoomCommand ToUpdateRoomStatusCommand(UpdateRoomStatusResource resource,
        int roomId)
    {
        return new UpdateStatusRoomCommand(roomId, resource.reservationId, resource.Status);
    }
}