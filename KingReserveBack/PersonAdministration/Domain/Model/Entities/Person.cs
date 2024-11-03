namespace KingReserveBack.PersonAdministration.Domain.Model.Entities;

public class Person
{
    public int Id { get; }
    public string Name { get; private set; }
    public DateOnly FechaReserva { get; private set; }
    public string Pais { get; private set; }
    public string Ciudad { get; private set; }
    public string Distrito  { get; private set; }
}