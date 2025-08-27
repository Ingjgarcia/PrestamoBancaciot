using MediatR;
using PrestamoBancario.Application.Auth.Command;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Application.Auth.Handler
{
    internal class RegistroUsuarioHandler : IRequestHandler<RegistroUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokens;
        public RegistroUsuarioHandler(IUsuarioRepository users, IPasswordHasher passwordHasher, ITokenService tokenService) 
        { _users = users;
            _hasher = passwordHasher;
            _tokens = tokenService;
        }
        public async Task<UsuarioDto> Handle(RegistroUsuarioCommand request, CancellationToken cancellationToken)
        {
            var req = request.Usuario;
            var user = new Usuario { Email = req.Email, Constrasena = _hasher.Hash(req.constrasena), Rol = req.IsAdmin ? Roles.Admin : Roles.User };
            await _users.AddAsync(user, cancellationToken);
            var token = _tokens.CreateToken(user);
            var response = new UsuarioDto
            {
                Token = token,
                Email = user.Email,
                Rol = user.Rol
            };

            return response;
        }
    }
}
