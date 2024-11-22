using KingReserveBack.PersonAdministration.Domain.Model.Commands;

namespace KingReserveBack.PersonAdministration.Domain.Model.Entities;

public partial class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateOnly Date { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Observations { get; set; }
    public int RoomId { get; set; }
    
    public Person(string name,
        int age,
        DateOnly date,
        string country,
        string city,
        string district,
        string observations,
        int roomId)
    {
        Name = name;
        Age = age;
        Date = date;
        Country = country;
        City = city;
        District = district;
        Observations = observations;
        RoomId = roomId;
    }
    
    public Person(CreatePersonCommand command)
        : this(command.Name, 
            command.Age, 
            command.Date,
            command.Country,
            command.City,
            command.District,
            command.Observations,
            command.RoomId){}
}