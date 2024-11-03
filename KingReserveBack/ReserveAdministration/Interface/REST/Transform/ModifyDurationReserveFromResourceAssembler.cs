using KingReserveBack.ReserveAdministration.Domain.Model.Commands;
using KingReserveBack.ReserveAdministration.Interface.REST.Resources;

namespace KingReserveBack.ReserveAdministration.Interface.REST.Transform;

public static class ModifyDurationReserveFromResourceAssembler
{
    public static ModifyDurationReserveCommand ToCommandFromResource(ModifyDurationReserveResource resource,
        int reserveId)
    {
        return new ModifyDurationReserveCommand(reserveId, resource.Duration);
    }
}