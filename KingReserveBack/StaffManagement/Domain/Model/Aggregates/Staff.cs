using KingReserveBack.StaffManagement.Domain.Model.Commands;

namespace KingReserveBack.StaffManagement.Domain.Model.Aggregates;

/**
 * Staff aggregate root entity
 */

public partial class Staff
{
    public int Id { get; }
    
    /*
     * campaign id
     */
    public string Reserves_id { get; private set; } //sin
    
    public string Name { get; private set; }
    public string Last_name { get; private set; }
    public string Email { get; private set; }
    
    public string On_job_status { get; private set; }
    
    public string Job_description { get; private set; }
    
    
    
    
    public Staff(string reserves_id, string name, string last_name, string job_description,  
        string email, string on_job_status)
    {
        Reserves_id = reserves_id;
        Name = name;
        Last_name = last_name;
        Job_description = job_description;
        Email = email;
        On_job_status = on_job_status;
        
    }

    public Staff(CreateStaffCommand command) : this(
        command.Reserves_id,
        command.Name,
        command.Last_name,
        command.Job_description,
        command.Email,
        command.On_job_status)
    {}
    
}
