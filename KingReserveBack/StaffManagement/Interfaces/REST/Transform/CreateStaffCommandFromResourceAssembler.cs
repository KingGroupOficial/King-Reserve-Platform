using KingReserveBack.StaffManagement.Domain.Model.Commands;
using KingReserveBack.StaffManagement.Interfaces.REST.Resources;

namespace KingReserveBack.StaffManagement.Interfaces.REST.Transform;

public static class CreateStaffCommandFromResourceAssembler
{
    public static CreateStaffCommand ToCommandFromResource(CreateStaffResource resource)
    {
        return new CreateStaffCommand(resource.Reserves_id, resource.Name, resource.Last_name,resource.Job_description, 
            resource.Email, resource.On_job_status);
    }

}