﻿namespace KingReserveBack.PersonAdministration.Interface.REST.Resources;

public record UpdatePersonResource(
    string Name,
    int Age,
    DateOnly Date,
    string Country,
    string City,
    string District,
    string Observations,
    int RoomId);