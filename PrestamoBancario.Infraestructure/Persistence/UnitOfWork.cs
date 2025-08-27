
using Microsoft.EntityFrameworkCore.Storage;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Infraestructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private IDbContextTransaction? _tx;
        public UnitOfWork(AppDbContext db) => _db = db;

        public async Task BeginTransactionAsync(CancellationToken ct)
            => _tx = await _db.Database.BeginTransactionAsync(ct);

        public async Task CommitAsync(CancellationToken ct)
        { if (_tx != null) { await _tx.CommitAsync(ct); await _tx.DisposeAsync(); _tx = null; } }

        public async Task RollbackAsync(CancellationToken ct)
        { if (_tx != null) { await _tx.RollbackAsync(ct); await _tx.DisposeAsync(); _tx = null; } }
    }
}

