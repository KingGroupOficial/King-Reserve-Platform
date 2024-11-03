namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources;

public record RoomResource(int Id, 
    string Name, 
    double Area , 
    string Status, 
    int ReservationId);