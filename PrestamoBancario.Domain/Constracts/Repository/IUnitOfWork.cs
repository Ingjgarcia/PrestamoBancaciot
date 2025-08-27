namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken ct);
        Task CommitAsync(CancellationToken ct);
        Task RollbackAsync(CancellationToken ct);
    }
}
