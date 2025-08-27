using Microsoft.EntityFrameworkCore;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;
using PrestamoBancario.Domain.Enums;
using PrestamoBancario.Infraestructure.Persistence;

namespace PrestamoBancario.Infrastructure.Persistence.Repositories;

internal class PrestamoRepository : IPrestamoRepository
{
    private readonly AppDbContext _db;
    public PrestamoRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Prestamo_ prestamo, CancellationToken ct)
    { await _db.Prestamos.AddAsync(prestamo, ct); await _db.SaveChangesAsync(ct); }

    public async Task<Prestamo_?> GetByIdAsync(Guid id, CancellationToken ct)
        => await _db.Prestamos.FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task UpdateAsync(Prestamo_ prestamo, CancellationToken ct)
    { _db.Prestamos.Update(prestamo); await _db.SaveChangesAsync(ct); }

    public async Task<IReadOnlyList<Prestamo_>> GetByUserAsync(Guid userId, CancellationToken ct)
        => await _db.Prestamos.Where(l => l.IdUsuario == userId).OrderByDescending(l => l.FechaCreacion).ToListAsync(ct);

    public async Task<IReadOnlyList<Prestamo_>> GetPendingAsync(CancellationToken ct)
        => await _db.Prestamos.Where(l => l.Estado == EstadoPrestamo.pendiente).OrderBy(l => l.FechaCreacion).ToListAsync(ct);

  
}
