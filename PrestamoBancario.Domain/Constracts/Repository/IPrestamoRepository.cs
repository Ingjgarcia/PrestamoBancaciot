using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IPrestamoRepository
    {
        Task<Prestamo?> GetByIdAsync(Guid id, CancellationToken ct);
        Task AddAsync(Prestamo prestamo, CancellationToken ct);
        Task UpdateAsync(Prestamo prestamo, CancellationToken ct);
        Task<IReadOnlyList<Prestamo>> GetByUserAsync(Guid idUsuario, CancellationToken ct);
        Task<IReadOnlyList<Prestamo>> GetAllAsync(CancellationToken ct);
        Task<IReadOnlyList<Prestamo>> GetPendingAsync(CancellationToken ct);
        
    }
}
