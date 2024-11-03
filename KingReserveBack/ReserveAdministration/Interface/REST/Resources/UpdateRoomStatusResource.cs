using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Resources;

public record UpdateRoomStatusResource(int reservationId,ERoomStatus Status);