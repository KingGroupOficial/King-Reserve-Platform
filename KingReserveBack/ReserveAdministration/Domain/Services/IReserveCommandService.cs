using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;

namespace KingReserveBack.ReserveAdministration.Domain.Services;

public interface IReserveCommandService
{
    Task<Reserve?> Handle(CreateReserveCommand command);
    Task<Reserve?> Handle(AddRoomToReserveCommand command);
    Task<Reserve?> Handle(ConcludeReserveCommand command);
    Task<Reserve?> Handle(ActiveReserveCommand command);
    Task<IEnumerable<Room>> Handle(DeleteRoomToReserveCommand command);
    Task<IEnumerable<Reserve>> Handle(DeleteReserveCommand command);
    Task<Reserve?> Handle(ModifyDurationReserveCommand command);
    Task<Reserve?> Handle(UpdateConditionReserveCommand command);
    Task<Room?> Handle(UpdateStatusRoomCommand command);
    Task<Room?> Handle(CreateRoomCommand command);
    Task<Reserve?> Handle(UpdateReserveCommand command);
    Task<Room?> Handle(UpdateRoomCommand command);
    
    
}