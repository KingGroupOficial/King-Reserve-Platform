using KingReserveBack.Shared.Domain.Repositories;
using KingReserveBack.StaffManagement.Domain.Model.Aggregates;

namespace KingReserveBack.StaffManagement.Domain.Repositories;


public interface IStaffRepository : IBaseRepository<Staff>
{
    Task<Staff?> FindStaffByEmailAsync(string email);
    
    new Task<Staff?> FindByIdAsync(int id);
}