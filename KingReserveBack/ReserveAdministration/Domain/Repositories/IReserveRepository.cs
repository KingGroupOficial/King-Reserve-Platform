using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;
using KingReserveBack.Shared.Domain.Repositories;

namespace KingReserveBack.ReserveAdministration.Domain.Repositories;

public interface IReserveRepository: IBaseRepository<Reserve>
{
    Task<IEnumerable<Reserve>> FindByUserIdAsync(UserId userId);
    Task<Reserve?> FindByReserveIdAndUserIdAsync(int reserveId, UserId userId);
    Task<IEnumerable<Room>> FindByReserveIdAsync(int reserveId);
    Task<Room?> FindByRoomIdAndReserveId(int roomId, int reserveId);
    void RemoveRoom(Room room);
}
