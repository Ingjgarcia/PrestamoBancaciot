using MediatR;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Application.Auth.Querys;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Domain.Constracts.Security;

namespace PrestamoBancario.Application.Auth.Handler
{
    internal class LoginHandler : IRequestHandler<LoginQuery, UsuarioDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokens;
        public LoginHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokens = tokenService;
        }
        public async Task<UsuarioDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var req = request.Usuario;
            var user = await _unitOfWork.Usuarios.GetByEmailAsync(req.Email, cancellationToken);
            if (user == null || !_unitOfWork.passwordHasher.Verify(req.constrasena, user.Constrasena)) throw new AccessViolationException("Usuario o contraseña no valido");

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
