using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;

public partial class Reserve
{
    public int Id { get; }
    public string Name { get; private set; }
    public DateOnly DateStart { get; private set; }
    public DateOnly DateEnd { get; private set; }
    
    
    
    public UserId UserId { get; }

    public Reserve(string _Name, 
        DateOnly _DateStart, 
        DateOnly? _DateEnd, 
        int userId) : this()
    {
        Name = _Name;
        DateStart = _DateStart;
        DateEnd = _DateEnd ?? DateStart;
        CalculateDuration();
        UserId = new UserId(userId);
    }
    
    public Reserve(CreateReserveCommand command)
        : this(command.Name, 
            command.DateStart, 
            command. DateEnd,
            command.userId){}
}