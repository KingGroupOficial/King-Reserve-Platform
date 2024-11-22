using KingReserveBack.IAM.Domain.Model.Commands;
using KingReserveBack.IAM.Interfaces.REST.Resources;

namespace KingReserveBack.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}