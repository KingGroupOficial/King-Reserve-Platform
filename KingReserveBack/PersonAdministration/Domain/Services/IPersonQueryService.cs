using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.PersonAdministration.Domain.Model.Queries;

namespace KingReserveBack.PersonAdministration.Domain.Services;

public interface IPersonQueryService
{
    Task<IEnumerable<Person>> Handle(GetAllPersonQuery query);
    Task<Person?> Handle(GetPersonbyIdRoom query);
    Task<Person?> Handle(GetPersonByIdQuery query);
}