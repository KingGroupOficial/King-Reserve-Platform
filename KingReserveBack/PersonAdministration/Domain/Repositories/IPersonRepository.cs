using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.Shared.Domain.Repositories;

namespace KingReserveBack.PersonAdministration.Domain.Repositories;

public interface IPersonRepository: IBaseRepository<Person>
{
    Task<Person?> FindByRoomIdAsync(int roomId);
    Task<Person?> FindByPersonIdAsync(int personId);
}