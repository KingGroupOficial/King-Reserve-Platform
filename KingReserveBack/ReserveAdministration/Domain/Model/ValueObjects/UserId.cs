namespace KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

public record  UserId(int Identifier)
{
    public UserId() : this(0)
    {
    }
}