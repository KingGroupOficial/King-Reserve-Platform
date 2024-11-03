using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
using KingReserveBack.StaffManagement.Interfaces.REST.Resources;

namespace KingReserveBack.StaffManagement.Interfaces.REST.Transform;

public static class StaffResourceFromEntityAssembler
{
    public static StaffResource ToResourceFromEntity(Staff entity)
    {
        return new StaffResource(entity.Id, entity.Reserves_id, entity.Name, entity.Last_name,entity.Job_description, 
            entity.Email, entity.On_job_status);
    }
    
}