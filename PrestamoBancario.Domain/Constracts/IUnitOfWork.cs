using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Constracts.Security;

namespace PrestamoBancario.Domain.Constracts
{
    internal interface IUnitOfWork
    {

        IPrestamoRepository Prestamos { get; }
        IUsuarioRepository Usuarios { get; }
        IPasswordHasher passwordHasher { get; }

        Task BeginTransactionAsync(CancellationToken ct);
        Task CommitAsync(CancellationToken ct);
        Task RollbackAsync(CancellationToken ct);
    }
}
