using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record CreateRoomCommand(string Name, double Area, int ReservationId, ERoomStatus status);