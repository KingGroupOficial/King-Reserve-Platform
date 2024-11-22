using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.PersonAdministration.Domain.Model.Queries;
using KingReserveBack.PersonAdministration.Domain.Repositories;
using KingReserveBack.PersonAdministration.Domain.Services;

namespace KingReserveBack.PersonAdministration.Application.Internal.QueryServices;

public class PersonQueryService(IPersonRepository personRepository):
    IPersonQueryService
{
    public async Task<IEnumerable<Person>> Handle(GetAllPersonQuery query)
    {
        return await personRepository.ListAsync();
    }

    public async Task<Person?> Handle(GetPersonbyIdRoom query)
    {
        return await personRepository.FindByRoomIdAsync(query.RoomId);
    }

    public async Task<Person?> Handle(GetPersonByIdQuery query)
    {
        return await personRepository.FindByPersonIdAsync(query.personId);
    }
}