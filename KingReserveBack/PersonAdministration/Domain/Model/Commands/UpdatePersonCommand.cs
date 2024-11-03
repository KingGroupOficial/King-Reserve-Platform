namespace KingReserveBack.PersonAdministration.Domain.Model.Commands;

public record UpdatePersonCommand(
    int PersonId,
    string Name,
    int Age,
    DateOnly Date,
    string Country,
    string City,
    string District,
    string Observations,
    int RoomId);