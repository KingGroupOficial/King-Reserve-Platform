using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Queries;

public record GetAllReserveByUserIdQuery(UserId UserId);