
using LoanManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Constracts.Security;
using PrestamoBancario.Infraestructure.Security;
using PrestamoBancario.Infrastructure.Persistence.Repositories;

namespace PrestamoBancario.Infraestructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private IDbContextTransaction? _tx;

        private IPrestamoRepository _prestamo;

        private IUsuarioRepository _usuario;

        public IPasswordHasher passwordHasher { get; }

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            passwordHasher = new PasswordHasher();
        }
        public IPrestamoRepository Prestamos
        {
            get { _prestamo ??= new PrestamoRepository(_db); return _prestamo; }
        }

        public IUsuarioRepository Usuarios
        {
            get { _usuario ??= new UsuarioRepository(_db); return _usuario; }
        }

        public async Task BeginTransactionAsync(CancellationToken ct)
            => _tx = await _db.Database.BeginTransactionAsync(ct);

        public async Task CommitAsync(CancellationToken ct)
        { if (_tx != null) { await _tx.CommitAsync(ct); await _tx.DisposeAsync(); _tx = null; } }

        public async Task RollbackAsync(CancellationToken ct)
        { if (_tx != null) { await _tx.RollbackAsync(ct); await _tx.DisposeAsync(); _tx = null; } }
    }
}

