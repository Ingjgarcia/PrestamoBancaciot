using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Repository
{
    internal interface ITokenService
    {
        string CreateToken(Usuario user);
    }
}
