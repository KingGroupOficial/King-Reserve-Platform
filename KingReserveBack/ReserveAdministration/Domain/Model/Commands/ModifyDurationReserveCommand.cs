namespace KingReserveBack.ReserveAdministration.Domain.Model.Commands;

public record ModifyDurationReserveCommand(int campaignId, int duration);