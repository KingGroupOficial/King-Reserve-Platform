using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
using KingReserveBack.StaffManagement.Domain.Model.Queries;

namespace KingReserveBack.StaffManagement.Domain.Services;

public interface IStaffQueryService
{
    Task<IEnumerable<Staff>> Handle(GetAllStaffsQuery query);
    
    Task<Staff?> Handle(GetStaffByEmailQuery query);
    
    Task<Staff?> Handle(GetStaffByIdQuery query);
}