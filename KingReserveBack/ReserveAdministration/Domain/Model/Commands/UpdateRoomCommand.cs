using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record UpdateRoomCommand(int ReserveId, int RoomId, string Name, double Area, ERoomStatus Status);