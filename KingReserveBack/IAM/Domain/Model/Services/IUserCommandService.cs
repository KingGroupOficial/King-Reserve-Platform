using KingReserveBack.IAM.Domain.Model.Aggregates;
using KingReserveBack.IAM.Domain.Model.Commands;

namespace KingReserveBack.IAM.Domain.Model.Services;

public interface IUserCommandService
{
    Task Handle(SignUpCommand command);
    Task<(User user, string token)> Handle(SignInCommand command);
}