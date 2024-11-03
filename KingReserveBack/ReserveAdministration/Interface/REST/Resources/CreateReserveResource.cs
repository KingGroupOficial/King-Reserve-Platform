namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources;

public record CreateReserveResource(string Name, 
    DateOnly DateStart, 
    DateOnly DateEnd,
    int userId);