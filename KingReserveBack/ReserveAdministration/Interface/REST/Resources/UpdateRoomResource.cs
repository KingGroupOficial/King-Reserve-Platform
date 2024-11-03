using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources;

public record UpdateRoomResource(string Name, Double Area, ERoomStatus Status);