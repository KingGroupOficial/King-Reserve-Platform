using System.Net.Mime;
using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Domain.Model.Queries;
using KingReserveBack.PersonAdministration.Domain.Services;
using KingReserveBack.PersonAdministration.Interface.REST.Resources;
using KingReserveBack.PersonAdministration.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KingReserveBack.PersonAdministration.Interface.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class PersonController(
    IPersonCommandService personCommandService,
    IPersonQueryService personQueryService):ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] CreatePersonResource createPersonResource)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createPersonCommand = CreatePersonCommandFromResourceAssembler.ToCommandFromResource(createPersonResource);
        var person = await personCommandService.Handle(createPersonCommand);
        if (person is null) return BadRequest();
        var resource = PersonResourceFromEntityAssembler.ToResourceFromEntity(person);
        return CreatedAtAction(nameof(GetPersonById), new { personId = person.Id }, resource);
    }
    
    [HttpGet("{personId:int}")]
    public async Task<IActionResult> GetPersonById([FromRoute] int personId)
    {
        var person = await personQueryService.Handle(new GetPersonByIdQuery(personId));
        if (person is null) return NotFound();
        var resource = PersonResourceFromEntityAssembler.ToResourceFromEntity(person);
        return Ok(resource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all persons",
        Description = "Get all persons in the system",
        OperationId="GetAllPersons"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The person", typeof(IEnumerable<PersonResource>))]
    public async Task<IActionResult> GetAllPersons()
    {
        var getAllPersonsQuery = new GetAllPersonQuery();
        var persons = await personQueryService.Handle(getAllPersonsQuery);
        var personResources = persons.Select(PersonResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(personResources);
    }
    
    [HttpDelete("{personId:int}")]
    public async Task<IActionResult> DeletePerson([FromRoute] int personId)
    {
        var person = await personQueryService.Handle(new GetPersonByIdQuery(personId));
        if (person is null) return NotFound();

        await personCommandService.Handle(new DeletePersonCommand(personId));
        return NoContent();
    }
    
    [HttpPut("{personId:int}")]
    public async Task<IActionResult> UpdatePerson(
        [FromRoute] int personId, 
        [FromBody] CreatePersonResource updatePersonResource)
    {
        var updatePersonCommand = UpdatePersonCommandFromResourceAssembler
            .ToCommandFromResource(personId, updatePersonResource);
        var person = await personCommandService.Handle(updatePersonCommand);
        if (person is null) return BadRequest();
        var resource = PersonResourceFromEntityAssembler.ToResourceFromEntity(person);
        return Ok(resource);
    }
}