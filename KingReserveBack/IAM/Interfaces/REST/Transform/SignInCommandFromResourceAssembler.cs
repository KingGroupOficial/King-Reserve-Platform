using KingReserveBack.IAM.Domain.Model.Commands;
using KingReserveBack.IAM.Interfaces.REST.Resources;

namespace KingReserveBack.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(resource.Username, resource.Password);
    }
}