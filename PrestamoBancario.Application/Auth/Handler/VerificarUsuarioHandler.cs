using MediatR;
using PrestamoBancario.Application.Auth.Command;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.Auth.Handler
{
    internal class VerificarUsuarioHandler : IRequestHandler<GetUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _users;
        public VerificarUsuarioHandler(IUsuarioRepository users, IPasswordHasher passwordHasher, ITokenService tokenService) 
        { _users = users;
        }
        public async Task<bool> Handle(GetUsuarioCommand request, CancellationToken cancellationToken)
        {

            var exists = await _users.GetByEmailAsync(request.email, cancellationToken);
            return (exists != null);
        }
    }
}
