using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.Queries;
using KingReserveBack.ReserveAdministration.Domain.Repositories;
using KingReserveBack.ReserveAdministration.Domain.Services;

namespace KingReserveBack.ReserveAdministration.Internal.QueryServices;

public class ReserveQueryService(
    IReserveRepository reserveRepository): 
    IReserveQueryService
{
    public async Task<IEnumerable<Room>> Handle(GetAllRoomByReserveIdQuery query)
    {
        return await reserveRepository.FindByReserveIdAsync(query.ReserveId);
    }

    public async Task<Room?> Handle(GetRoomByIdAndReserveIdQuery query)
    {
        return await reserveRepository.FindByRoomIdAndReserveId(query.RoomId, query.ReserveId);
    }

    public async Task<Reserve?> Handle(GetReserveByIdAndUserIdQuery query)
    {
        return await reserveRepository.FindByReserveIdAndUserIdAsync(query.ReserveId, query.UserId);
    }

    public async Task<Reserve?> Handle(GetReserveByIdQuery query)
    {
        return await reserveRepository.FindByIdAsync(query.ReserveId);
    }

    public async Task<IEnumerable<Reserve>> Handle(GetAllReserveQuery query)
    {
        return await reserveRepository.ListAsync();
    }

    public  async Task<IEnumerable<Reserve>> Handle(GetAllReserveByUserIdQuery query)
    {
        return await reserveRepository.FindByUserIdAsync(query.UserId);
    }
}