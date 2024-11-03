using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.ValueObjects;

namespace KingReserveBack.ReserveAdministration.Domain.Model.Entities
{
    public partial class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }
        public int ReserveId { get; set; }
        public ERoomStatus Status { get; set; }

        public Room(string name, double area, int reserveId, ERoomStatus status)
        {
            Name = name;
            Area = area;
            ReserveId = reserveId;
            Status = status;
        }

        public Room(CreateRoomCommand command) : this(command.Name, command.Area, command.ReservationId, command.status) { }
    }
}