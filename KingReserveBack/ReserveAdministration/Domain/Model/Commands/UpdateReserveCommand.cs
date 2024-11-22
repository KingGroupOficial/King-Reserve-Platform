namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record UpdateReserveCommand(
    int reserveId,
    string name, 
    DateOnly dateStart, 
    DateOnly dateEnd);