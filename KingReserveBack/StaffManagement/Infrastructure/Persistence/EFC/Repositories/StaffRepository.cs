using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Repositories;
using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
using KingReserveBack.StaffManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KingReserveBack.StaffManagement.Infrastructure.Persistence.EFC.Repositories;

public class StaffRepository(AppDbContext context) : BaseRepository<Staff>(context), IStaffRepository
{
    public Task<Staff?> FindStaffByEmailAsync(string email)
    {
        return Context.Set<Staff>().Where(p=>p.Email == email).FirstOrDefaultAsync();
    }

    public new Task<Staff?> FindByIdAsync(int id)
    {
        return Context.Set<Staff>().Where(p=>p.Id == id).FirstOrDefaultAsync();
    }
    
}