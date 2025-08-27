using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IPrestamoRepository
    {
        Task<Prestamo_?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Prestamo_ prestamo, CancellationToken ct);
        Task UpdateAsync(Prestamo_ prestamo, CancellationToken ct);
        Task<IReadOnlyList<Prestamo_>> GetByUserAsync(Guid idUsuario, CancellationToken ct);
        Task<IReadOnlyList<Prestamo_>> GetPendingAsync(CancellationToken ct);
    }
}
