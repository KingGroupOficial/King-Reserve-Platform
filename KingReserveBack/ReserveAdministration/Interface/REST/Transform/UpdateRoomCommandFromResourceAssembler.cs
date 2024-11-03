using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform
{
    public class UpdateRoomCommandFromResourceAssembler
    {
        public static UpdateRoomCommand ToCommandFromResource(
            int reservationId,
            int roomId,
            UpdateRoomResource resource)
        {
            return new UpdateRoomCommand(reservationId,
                roomId,
                resource.Name,
                resource.Area,
                resource.Status);
        }
    }
}