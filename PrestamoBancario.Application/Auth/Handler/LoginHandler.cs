using MediatR;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Application.Auth.Querys;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.Auth.Handler
{
    internal class LoginHandler : IRequestHandler<LoginQuery, UsuarioDto>
    {
        private readonly IUsuarioRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokens;
        public LoginHandler(IUsuarioRepository users, IPasswordHasher passwordHasher, ITokenService tokenService)
        {
            _users = users;
            _hasher = passwordHasher;
            _tokens = tokenService;
        }
        public async Task<UsuarioDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var req = request.Usuario;
            var user = await _users.GetByEmailAsync(req.Email, cancellationToken);
            if (user == null || !_hasher.Verify(req.constrasena, user.Constrasena)) return new UsuarioDto();


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
