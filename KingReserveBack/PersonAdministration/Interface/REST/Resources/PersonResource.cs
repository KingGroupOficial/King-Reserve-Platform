namespace KingReserveBack.PersonAdministration.Interface.REST.Resources;

public record PersonResource(
    int personId,
    string Name,
    int Age,
    DateOnly Date,
    string Country,
    string City,
    string District,
    string Observations,
    int RoomId);