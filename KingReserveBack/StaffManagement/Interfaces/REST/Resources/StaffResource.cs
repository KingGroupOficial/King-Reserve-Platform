namespace KingReserveBack.StaffManagement.Interfaces.REST.Resources;

public record StaffResource(int Id, string Reserves_id, string Name, string Last_name,string Job_description, string Email, string On_job_status);