using KingReserveBack.PersonAdministration.Domain.Model.Commands;
using KingReserveBack.PersonAdministration.Interface.REST.Resources;

namespace KingReserveBack.PersonAdministration.Interface.REST.Transform;

public static class DeletePersonCommandFromResourceAssembler
{
    public static DeletePersonCommand ToCommandFromResource(
        int personId, DeletePersonResource resource)
    {
        return new DeletePersonCommand(personId);
    }
}