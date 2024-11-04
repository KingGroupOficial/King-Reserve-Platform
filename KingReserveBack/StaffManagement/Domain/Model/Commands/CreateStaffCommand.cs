namespace KingReserveBack.StaffManagement.Domain.Model.Commands;

public record CreateStaffCommand(string Reserves_id, string Name, string Last_name,string Job_description, 
    string Email, string On_job_status);