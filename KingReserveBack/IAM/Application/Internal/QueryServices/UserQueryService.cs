using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.IAM.Domain.Model.Queries;
using KingReserveBack.IAM.Domain.Model.Services;
using KingReserveBack.IAM.Domain.Repositories;

namespace KingReserveBack.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }
    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }
    public async Task<User?> Handle(GetUserByUsernameQuery query)
    {
        return await userRepository.FindByUsernameAsync(query.Username);
    }
}