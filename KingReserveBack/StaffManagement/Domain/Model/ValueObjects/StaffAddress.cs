namespace KingReserveBack.StaffManagement.Domain.Model.ValueObjects;

public record StaffAddress(string Street, string Number, string City, string PostalCode, string Country)
{
    public StaffAddress() : this(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty)
    {
    }

    public StaffAddress(String street) : this(street, string.Empty, string.Empty, string.Empty,
        string.Empty)
    {
    }

    public StaffAddress(string street, string number, string city) : this(street, number, city,
        string.Empty, string.Empty)
    {
    }
    
    public StaffAddress(string street, string number, string city, string postalCode) : this(street, number, city, postalCode,
        string.Empty)
    {
    }
    
    public string FullAddress => $"{Street} {Number}, {City}, {PostalCode}, {Country}";
}