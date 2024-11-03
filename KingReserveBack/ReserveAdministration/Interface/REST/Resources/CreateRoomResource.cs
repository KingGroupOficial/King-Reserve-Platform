using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources
{
    public record CreateRoomResource(string Name, double Area, int ReservationId, ERoomStatus Status);
}