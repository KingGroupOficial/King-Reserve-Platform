namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record CreateReserveCommand(string Name, 
    DateOnly DateStart, DateOnly DateEnd, int userId);