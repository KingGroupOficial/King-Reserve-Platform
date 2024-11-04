namespace KingReserveBack.PersonAdministration.Domain.Model.Commands;

public record CreatePersonCommand(
    string Name,
    int Age,
    DateOnly Date,
    string Country,
    string City,
    string District,
    string Observations,
    int RoomId
);
