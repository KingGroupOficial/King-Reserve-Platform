using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.PersonAdministration.Domain.Repositories;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingReserveBack.PersonAdministration.Infrastructure.Persistence.EFC.Repositories;

public class PersonRepository(AppDbContext context):
    BaseRepository<Person>(context), IPersonRepository
{
    public async Task<Person?> FindByRoomIdAsync(int roomId)
    {
        return await Context.Set<Person>()
            .Where(c => c.RoomId == roomId)
            .FirstOrDefaultAsync();
    }
    public async Task<Person?> FindByPersonIdAsync(int personId)
    {
        return await Context.Set<Person>()
            .FirstOrDefaultAsync(c => c.Id == personId);
    }
    public new async Task<Person?> FindByIdAsync(int id)
    {
        return await Context.Set<Person>()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public new async Task<IEnumerable<Person>> ListAsync()
    {
        return await Context.Set<Person>()
            .ToListAsync();
    }
}