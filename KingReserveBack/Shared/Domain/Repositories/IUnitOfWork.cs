namespace KingReserveBack.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}