using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.IAM.Domain.Model.Queries;

namespace KingReserveBack.IAM.Domain.Model.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByUsernameQuery query);
}