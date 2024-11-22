using System.Net.Mime;
using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Domain.Model.Queries;
using KingReserveBack.ReserveAdministration.Domain.Services;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;
using KingReserveBack.ReserveAdministration.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KingReserveBack.ReserveAdministration.Interface.REST;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ReserveController(
    IReserveCommandService reserveCommandService, 
    IReserveQueryService reserveQueryService):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReserve([FromBody] 
        CreateReserveResource createReserveResource)
    {
        var createReserveCommand =
            CreateReserveCommandFromResourceAssembler
                .ToCommandFromResource(createReserveResource);
        var reserve = await reserveCommandService.Handle(createReserveCommand);
        if (reserve is null) return BadRequest();
        var resource = ReserveResourceFromEntityAssembler
            .ToResourceFromEntity(reserve);
        return CreatedAtAction(nameof(GetReserveById), new { reserveId = resource.Id }, resource);
    }
    
    [HttpGet("{reserveId:int}")]
    public async Task<IActionResult> GetReserveById([FromRoute] int reserveId)
    {
        var reserve = await reserveQueryService.Handle(new GetReserveByIdQuery(reserveId));
        if (reserve is null) return NotFound();
        var resource = ReserveResourceFromEntityAssembler.ToResourceFromEntity(reserve);
        return Ok(resource);
    }
    
    [HttpPut("{reserveId:int}/modify-duration")]
    public async Task<IActionResult> ModifyReserveDuration([FromRoute] int reserveId, [FromBody] ModifyDurationReserveResource modifyDurationReserveResource)
    {
        var modifyDurationReserveCommand = ModifyDurationReserveFromResourceAssembler.ToCommandFromResource(modifyDurationReserveResource, reserveId);
        var reserve = await reserveCommandService.Handle(modifyDurationReserveCommand);
        if (reserve is null) return BadRequest();
        var resource = ReserveResourceFromEntityAssembler.ToResourceFromEntity(reserve);
        return CreatedAtAction(nameof(GetReserveById), new { reserveId = resource.Id }, resource);
    }
    
    [HttpPost("add-room")]
    public async Task<IActionResult> AddRoomToReserve([FromBody] CreateRoomResource createRoomResource)
    {
        try
        {
            // Log the incoming request
            Console.WriteLine($"Received request to add room: {createRoomResource}");

            var createRoomCommand = CreateRoomCommandFromResourceAssembler.ToCommandFromResource(createRoomResource);
        
            // Log the command transformation
            Console.WriteLine($"Transformed to command: {createRoomCommand}");

            var room = await reserveCommandService.Handle(createRoomCommand);
            if (room is null)
            {
                // Log the failure
                Console.WriteLine("Failed to create room: Command service returned null.");
                return BadRequest();
            }

            var resource = RoomResourceFromEntityAssembler.ToResourceFromEntity(room);
            var rId = resource.ReservationId;
            return CreatedAtAction(nameof(GetRoomByIdAndReserveId), new { reserveId = rId ,roomId = resource.Id }, createRoomResource);
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Exception occurred: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    
    [HttpGet("{reserveId:int}/rooms/{roomId:int}")]
    public async Task<IActionResult> GetRoomByIdAndReserveId([FromRoute] int reserveId, [FromRoute] int roomId)
    {
        var room = await reserveQueryService
            .Handle(new GetRoomByIdAndReserveIdQuery(
                reserveId, roomId));
        if (room is null) return NotFound();
        var resource = RoomResourceFromEntityAssembler
            .ToResourceFromEntity(room);
        return Ok(resource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all reserves",
        Description = "Get all reserves",
        OperationId = "GetAllReserves"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The reserves", typeof(IEnumerable<ReserveResource>))]
    public async Task<IActionResult> GetAllReserves()
    {
        var getAllReserveQuery = new GetAllReserveQuery();
        var reserves = await reserveQueryService.Handle(getAllReserveQuery);
        var reserveResources = reserves
            .Select(ReserveResourceFromEntityAssembler
                .ToResourceFromEntity);
        return Ok(reserveResources);
    }
    
    [HttpGet("{reserveId:int}/rooms")]
    [SwaggerOperation(
        Summary = "Get all rooms by reserve ID",
        Description = "Get all rooms by reserve ID",
        OperationId = "GetAllRoomsByReserveId"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The rooms", typeof(IEnumerable<RoomResource>))]
    public async Task<IActionResult> GetAllRoomsByReserveId([FromRoute] int reserveId)
    {
        var getAllRoomByReserveIdQuery = new GetAllRoomByReserveIdQuery(reserveId);
        var rooms = await reserveQueryService.Handle(getAllRoomByReserveIdQuery);
        var roomResources = rooms.Select(RoomResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(roomResources);
    }

    [HttpDelete("{reserveId:int}")]
    public async Task<IActionResult> DeleteReserve
        ([FromRoute] int reserveId)
    {
        var reserves = await reserveCommandService
            .Handle(new DeleteReserveCommand(reserveId));
        var resources = reserves
            .Select(ReserveResourceFromEntityAssembler
                .ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpDelete("{reserveId:int}/room")]
    public async Task<IActionResult> DeleteRoomToReserve
        ([FromRoute] int reserveId,
            [FromBody] DeleteRoomResource deleteRoomResource)
    {
        var deleteRoomToReserveCommand = 
            DeleteRoomCommandFromResourceAssembler
                .ToCommandFromResource(reserveId, deleteRoomResource);
        var rooms = await reserveCommandService
            .Handle(deleteRoomToReserveCommand);
        var resources = rooms
            .Select(RoomResourceFromEntityAssembler
                .ToResourceFromEntity);
        return Ok(resources);    
    }
    
    [HttpPut("{reserveId:int}")]
    public async Task<IActionResult> UpdateReserve(
        [FromRoute] int reserveId, 
        [FromBody] CreateReserveResource updateReserveResource)
    {
        var updateReserveCommand =
            UpdateReserveCommandFromResourceAssembler
                .ToCommandFromResource(reserveId,updateReserveResource);
        var reserve = await reserveCommandService
            .Handle(updateReserveCommand);
        if (reserve == null) return BadRequest();
        var resource = ReserveResourceFromEntityAssembler
            .ToResourceFromEntity(reserve);
        return Ok(resource);
    }
    
    [HttpPut("{reserveId:int}/rooms/{roomId:int}")]
    public async Task<IActionResult> UpdateRoom(
        [FromRoute] int reserveId, 
        [FromRoute] int roomId,
        [FromBody] UpdateRoomResource updateRoomResource)
    {
        var updateRoomCommand =
            UpdateRoomCommandFromResourceAssembler
                .ToCommandFromResource(reserveId,
                    roomId, 
                    updateRoomResource);
        var room = await reserveCommandService
            .Handle(updateRoomCommand);
        if (room == null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler
            .ToResourceFromEntity(room);
        return Ok(resource);
    }
    
    [HttpPut("{reserveId:int}/condition-finished")]
    public async Task<IActionResult> ConcludeReserve(
        [FromRoute] int reserveId)
    {
        var concludeReserveCommand = 
            new ConcludeReserveCommand(reserveId);
        var reserve = await reserveCommandService
            .Handle(concludeReserveCommand);
        if (reserve == null) return BadRequest();
        var resource = ReserveResourceFromEntityAssembler
            .ToResourceFromEntity(reserve);
        return Ok(resource);
    }

    [HttpPut("{reserveId:int}/condition-active")]
    public async Task<IActionResult> ActiveReserve(
        [FromRoute] int reserveId)
    {
        var activeReserveCommand = 
            new ActiveReserveCommand(reserveId);
        var reserve = await reserveCommandService.Handle(activeReserveCommand);
        if (reserve == null) return BadRequest();
        var resource = ReserveResourceFromEntityAssembler
            .ToResourceFromEntity(reserve);
        return Ok(resource);
    }
    
    [HttpPut("{roomId:int}/status")]
    public async Task<IActionResult> UpdateRoomStatus(
        [FromRoute] int roomId, 
        [FromBody] UpdateRoomStatusResource updateRoomStatusResource)
    {
        var updateRoomStatusCommand =
            UpdateRoomStatusCommandFromResourceAssembler
                .ToUpdateRoomStatusCommand(updateRoomStatusResource, roomId);
        var room = await reserveCommandService
            .Handle(updateRoomStatusCommand);
        if (room == null) return BadRequest();
        var resource = RoomResourceFromEntityAssembler
            .ToResourceFromEntity(room);
        return Ok(resource);
    }
}