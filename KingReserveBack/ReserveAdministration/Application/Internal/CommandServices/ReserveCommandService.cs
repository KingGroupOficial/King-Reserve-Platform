using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Repositories;
using KingReserveBack.ReserveAdministration.Domain.Services;
using KingReserveBack.Shared.Domain.Repositories;

namespace KingReserveBack.ReserveAdministration.Internal.CommandServices;

public class ReserveCommandService : IReserveCommandService
{
    private readonly IReserveRepository _reserveRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ReserveCommandService(IReserveRepository reserveRepository,
        IUnitOfWork unitOfWork)
    {
        _reserveRepository = reserveRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Reserve?> Handle(CreateReserveCommand command)
    {
        var reserve = new Reserve(command);

        try
        {
            await _reserveRepository.AddAsync(reserve);
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while creating a reserve: {e.Message}");
            return null;
        }
    }

    public async Task<Reserve?> Handle(AddRoomToReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.reserveId);
        if (reserve is null) throw new Exception("Reserve not found");
        var room = await _reserveRepository.FindByRoomIdAndReserveId(command.roomId, reserve.Id);
        reserve.addRoom(room);

        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Reserve?> Handle(ConcludeReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.ReserveId);
        reserve.ConditionFinished();
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Reserve?> Handle(ActiveReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.ReserveId);
        reserve.ConditionActive();
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Room>> Handle(DeleteRoomToReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.reserveId);
        var roomRemove = reserve.Rooms.FirstOrDefault(room => room.Id == command.roomId);
        reserve.removeRoom(command.roomId);
        var rooms = reserve.Rooms;
        try
        {
            await _unitOfWork.CompleteAsync();
            return rooms;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<IEnumerable<Reserve>> Handle(DeleteReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.ReserveId);
        _reserveRepository.Remove(reserve);
        var reserves = await _reserveRepository.ListAsync();
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserves;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Reserve?> Handle(ModifyDurationReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.campaignId);
        if (command.duration < 0) throw new Exception("Duration must be greater than 0");
        reserve.ModifyDuration(command.duration);
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Reserve?> Handle(UpdateConditionReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.reserveId);
        reserve.UpdateCondition(command.condition);
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Room?> Handle(UpdateStatusRoomCommand command)
    {
        var room = await _reserveRepository.FindByRoomIdAndReserveId(command.RoomId, command.ReservationId);
        room.Status = command.Status;
        try
        {
            await _unitOfWork.CompleteAsync();
            return room;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Room?> Handle(CreateRoomCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.ReservationId);
        if (reserve is null) throw new Exception("Reserve not found");

        var room = new Room(command);
        reserve.addRoom(room);

        try
        {
            await _unitOfWork.CompleteAsync();
            return room;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Reserve?> Handle(UpdateReserveCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.reserveId);
        reserve.UpdateInformation(command);
        try
        {
            await _unitOfWork.CompleteAsync();
            return reserve;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Room?> Handle(UpdateRoomCommand command)
    {
        var reserve = await _reserveRepository.FindByIdAsync(command.ReserveId);
        var room = reserve.Rooms.FirstOrDefault(r => r.Id == command.RoomId);
        if (room == null) throw new Exception("Room not found");

        room.Name = command.Name;
        room.Area = command.Area;
        room.Status = command.Status;

        try
        {
            await _unitOfWork.CompleteAsync();
            return room;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}