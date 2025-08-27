using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IPrestamoRepository
    {
        Task<Prestamo?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Prestamo loan, CancellationToken ct);
        Task UpdateAsync(Prestamo loan, CancellationToken ct);
        Task<IReadOnlyList<Prestamo>> GetByUserAsync(Guid userId, CancellationToken ct);
        Task<IReadOnlyList<Prestamo>> GetPendingAsync(CancellationToken ct);
    }
}
