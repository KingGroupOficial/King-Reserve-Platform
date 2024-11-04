using KingReserveBack.PersonAdministration.Domain.Model.Commands;

namespace KingReserveBack.PersonAdministration.Domain.Model.Entities;

public partial class Person
{
    public void UpdateInformationPerson(UpdatePersonCommand command)
    {
        this.Name = command.Name;
        this.Age = command.Age;
        this.Date = command.Date;
        this.Country = command.Country;
        this.City = command.City;
        this.District = command.District;
        this.Observations = command.Observations;
        this.RoomId = command.RoomId;
    }
}