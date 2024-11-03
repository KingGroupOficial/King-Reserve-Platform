using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
using KingReserveBack.StaffManagement.Domain.Model.Commands;

namespace KingReserveBack.StaffManagement.Domain.Services;

public interface IStaffCommandService
{
    Task<Staff?> Handle(CreateStaffCommand command);

    Task<Staff?> Handle(DeleteStaffCommand command);
}