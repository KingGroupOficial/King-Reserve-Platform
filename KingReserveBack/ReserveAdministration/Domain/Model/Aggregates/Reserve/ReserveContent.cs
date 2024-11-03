

using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;

public partial class Reserve
{
    public EReserveCondition Condition { get; protected set; }
    public int Duration { get; protected set; }
    public ICollection<Room> Rooms { get; set; }
    
    public Reserve()
    {
        Rooms = new List<Room>();
        Condition = EReserveCondition.Inactive;
        UserId = new UserId(); 
      
    }
    
    public void ConditionActive()
    {
        Condition = EReserveCondition.Active;
    }
    
    public void ConditionInactive()
    {
        Condition = EReserveCondition.Inactive;
    }
    
    public void ConditionFinished()
    {
        Condition = EReserveCondition.Finished;
    }
    
    public void UpdateCondition(EReserveCondition condition)
    {
        Condition = condition;
    }
    
    public void UpdateInformation(UpdateReserveCommand command)
    {
        this.Name = command.name;
        this.DateStart = command.dateStart;
        this.DateEnd = command.dateEnd;
        this.CalculateDuration();
        var date = new DateOnly();
        if (date >= DateEnd)
        {
            this.UpdateCondition("Finished");

        }
    }
    
    public void UpdateCondition(string condition)
    {
      
            switch (condition)
            {
                case "Inactive":
                   ConditionInactive();
                    break;
                case "Active":
                   ConditionActive();
                    break;
                case "Finished":
                    ConditionFinished();
                    break;
                default:
                    throw new ArgumentException("Estado no válido", nameof(condition));
            }
        
    }

    private void CalculateDuration()
    {
        Duration = (DateEnd.DayNumber - DateStart.DayNumber);
    }
    
    public void addRoom(Room room)
    {
        if (room != null)
        {
            Rooms.Add(room);
        }
    }
    
    public void removeRoom(int room)
    {
        var roomToRemove = Rooms.FirstOrDefault(r => r.Id == room);
        if(roomToRemove != null)
            Rooms.Remove(roomToRemove); 
    }
    
    
    
    public void ClearRooms() => Rooms.Clear();
    
    
    public void ModifyDuration(int duration)
    {
        Duration = duration;
        DateEnd = DateStart.AddDays(duration);
    }
}