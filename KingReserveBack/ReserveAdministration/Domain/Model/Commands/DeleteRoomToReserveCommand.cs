namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record DeleteRoomToReserveCommand(int reserveId, int roomId);