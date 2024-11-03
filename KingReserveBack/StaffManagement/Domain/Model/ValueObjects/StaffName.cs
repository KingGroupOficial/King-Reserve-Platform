namespace KingReserveBack.StaffManagement.Domain.Model.ValueObjects;

public record StaffName(string Name, string Last_name)
{
    public StaffName() : this(string.Empty, string.Empty)
    {
    }

    public StaffName(string name) : this(name, string.Empty)
    {
    }

    public string FullName => $"{Name} {Last_name}";
};