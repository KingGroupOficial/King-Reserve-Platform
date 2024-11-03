using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Domain.Model.Entities;

namespace KingReserveBack.PersonAdministration.Domain.Services;

public interface IPersonCommandService
{
    Task<Person?> Handle(CreatePersonCommand command);
    Task<Person?> Handle(UpdatePersonCommand command);
    Task<IEnumerable<Person>> Handle(DeletePersonCommand command);
}