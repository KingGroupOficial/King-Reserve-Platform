using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record UpdateStatusRoomCommand(int RoomId,int ReservationId, ERoomStatus Status);