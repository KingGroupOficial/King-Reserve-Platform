using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Queries;

public record GetReserveByIdAndUserIdQuery(int ReserveId, UserId UserId);