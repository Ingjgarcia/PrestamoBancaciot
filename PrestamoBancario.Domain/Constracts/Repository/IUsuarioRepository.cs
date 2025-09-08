using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface IUsuarioRepository
    {
            Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct);
            Task<Usuario?> GetByIdAsync(long id, CancellationToken ct);
            Task AddAsync(Usuario user, CancellationToken ct);
    }
}
