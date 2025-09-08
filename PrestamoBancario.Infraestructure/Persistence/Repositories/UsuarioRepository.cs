using Microsoft.EntityFrameworkCore;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;
using PrestamoBancario.Infraestructure.Persistence;

namespace LoanManagement.Infrastructure.Persistence.Repositories;

internal class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _db;
    public UsuarioRepository(AppDbContext db) => _db = db;

    public async Task AddAsync(Usuario user, CancellationToken ct)
    { await _db.Usuarios.AddAsync(user, ct); await _db.SaveChangesAsync(ct); }

    public Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct)
        => _db.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);

    public Task<Usuario?> GetByIdAsync(long id, CancellationToken ct)
        => _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id, ct);
}
