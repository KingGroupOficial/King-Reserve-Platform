using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
using KingReserveBack.StaffManagement.Domain.Model.Queries;
using KingReserveBack.StaffManagement.Domain.Repositories;
using KingReserveBack.StaffManagement.Domain.Services;

namespace KingReserveBack.StaffManagement.Application.Internal.QueryServices;

public class StaffQueryService(IStaffRepository staffRepository) : IStaffQueryService
{
    public async Task<IEnumerable<Staff>> Handle(GetAllStaffsQuery query)
    {
        return await staffRepository.ListAsync();
    }

    public async Task<Staff?> Handle(GetStaffByEmailQuery query)
    {
        return await staffRepository.FindStaffByEmailAsync(query.Email);
    }

    public async Task<Staff?> Handle(GetStaffByIdQuery query)
    {
        return await staffRepository.FindByIdAsync(query.StaffId);
    }

}