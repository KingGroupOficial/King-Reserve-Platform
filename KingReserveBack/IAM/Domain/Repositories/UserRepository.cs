using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.Shared.Domain.Repositories;

namespace KingReserveBack.IAM.Domain.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> FindByUsernameAsync(string username);
    bool ExistsByUsername(string username);
}