namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record AddRoomToReserveCommand(int reserveId, int roomId);