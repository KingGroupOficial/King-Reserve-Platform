using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.Queries;

namespace KingReserveBack.ReserveAdministration.Domain.Services;

public interface IReserveQueryService
{
    Task<IEnumerable<Room>> Handle(GetAllRoomByReserveIdQuery query);
    Task<Room?> Handle(GetRoomByIdAndReserveIdQuery query);
    Task<Reserve?> Handle(GetReserveByIdAndUserIdQuery query);
    Task<Reserve?> Handle(GetReserveByIdQuery query);
    Task<IEnumerable<Reserve>> Handle(GetAllReserveQuery query);
    Task<IEnumerable<Reserve>> Handle(GetAllReserveByUserIdQuery query);
    
}
