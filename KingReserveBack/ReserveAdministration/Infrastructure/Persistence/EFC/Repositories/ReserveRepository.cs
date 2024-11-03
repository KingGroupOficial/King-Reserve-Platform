using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;
using KingReserveBack.ReserveAdministration.Domain.Repositories;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingReserveBack.ReserveAdministration.Infrastructure.Persistence.EFC.Repositories;

public class ReserveRepository(AppDbContext context): BaseRepository<Reserve>(context), IReserveRepository
{
    public async Task<IEnumerable<Reserve>> 
        FindByUserIdAsync(UserId userId)
    {
        return await Context.Set<Reserve>()
            .Where(c=>c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Reserve?> 
        FindByReserveIdAndUserIdAsync(int reserveId, UserId userId)
    {
        return await Context.Set<Reserve>()
            .FirstOrDefaultAsync
                (c => c.Id == reserveId && c.UserId == userId);
    }

    public async Task<IEnumerable<Room>> 
        FindByReserveIdAsync(int reserveId)
    {
        var reserve = await Context.Set<Reserve>()
            .Include(c => c.Rooms)
            .FirstOrDefaultAsync(c => c.Id == reserveId);
        return reserve.Rooms;
    }

    public async Task<Room?> 
        FindByRoomIdAndReserveId(int roomId, int reserveId)
    {
        var reserve = await Context.Set<Reserve>()
            .Include(c => c.Rooms)
            .FirstOrDefaultAsync(c => c.Id == reserveId);
        var room = reserve?.Rooms.FirstOrDefault(b => b.Id == roomId);
        if(room != null)
        {
            Console.WriteLine($"Room ID: {room.Id}, Name: {room.Name}");
        }
        return room;
    }

    public void RemoveRoom(Room room)
    {
        Context.Set<Room>().Remove(room);
    }
    
    public new async Task<Reserve?> FindByIdAsync(int id)
    {
        return await Context.Set<Reserve>()
            .Include(r => r.Rooms)
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    
    public new async Task<IEnumerable<Reserve>> ListAsync()
    {
        return await Context.Set<Reserve>()
            .Include(r => r.Rooms)
            .ToListAsync();
    }
}