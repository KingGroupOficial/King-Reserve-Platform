namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources;

public record ReserveResource(int Id,string Name,
    DateOnly DateStart,
    DateOnly DateEnd, 
    string Condition, 
    int Duration, 
    int userId
    );