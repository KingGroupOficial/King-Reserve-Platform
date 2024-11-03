using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform
{
    public static class CreateRoomCommandFromResourceAssembler
    {
        public static CreateRoomCommand ToCommandFromResource(CreateRoomResource resource)
        {
            return new CreateRoomCommand(resource.Name, resource.Area, resource.ReservationId, resource.Status);
        }
    }
}