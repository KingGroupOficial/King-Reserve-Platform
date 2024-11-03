using System.Net.Mime;
using KingReserveBack.StaffManagement.Domain.Model.Commands;
using KingReserveBack.StaffManagement.Domain.Model.Queries;
using KingReserveBack.StaffManagement.Domain.Services;
using KingReserveBack.StaffManagement.Interfaces.REST.Resources;
using KingReserveBack.StaffManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace KingReserveBack.StaffManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class StaffsController(IStaffCommandService staffCommandService, IStaffQueryService staffQueryService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateStaff(CreateStaffResource resource)
    {
        var createStaffCommand = CreateStaffCommandFromResourceAssembler.ToCommandFromResource(resource);
        var staff = await staffCommandService.Handle(createStaffCommand);
        if (staff is null) return BadRequest();
        var staffResource = StaffResourceFromEntityAssembler.ToResourceFromEntity(staff);
        return CreatedAtAction(nameof(GetStaffById), new { staffId = staffResource.Id}, staffResource);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStaffs()
    {
        var getAllStaffsQuery = new GetAllStaffsQuery();
        var staffs = await staffQueryService.Handle(getAllStaffsQuery);
        var staffResources = staffs.Select(StaffResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(staffResources);
    }

    [HttpGet("{staffId:int}")]
    public async Task<IActionResult> GetStaffById(int staffId)
    {
        var getStaffByIdQuery = new GetStaffByIdQuery(staffId);
        var staff = await staffQueryService.Handle(getStaffByIdQuery);
        if (staff == null) return NotFound();
        var staffResource = StaffResourceFromEntityAssembler.ToResourceFromEntity(staff);
        return Ok(staffResource);
    }
    
    [HttpPut("{staffId:int}")]
    
    
    [HttpDelete("{staffId:int}")]
    public async Task<IActionResult> DeleteStaff([FromRoute] int staffId)
    {
        var staff = await staffCommandService.Handle(new DeleteStaffCommand(staffId));
        if (staff is null) return BadRequest();
        return Ok();
    }
    
}