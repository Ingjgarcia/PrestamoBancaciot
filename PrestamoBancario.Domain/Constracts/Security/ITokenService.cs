using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Domain.Constracts.Security
{
    internal interface ITokenService
    {
        string CreateToken(Usuario user);
    }
}
