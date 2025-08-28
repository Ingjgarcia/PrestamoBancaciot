using MediatR;
using PrestamoBancario.Application.Auth.Command;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Domain.Constracts.Security;
using PrestamoBancario.Domain.Entities;
using System.Net.Mail;

namespace PrestamoBancario.Application.Auth.Handler
{
    internal class RegistroUsuarioHandler : IRequestHandler<RegistroUsuarioCommand, UsuarioDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokens;
        public RegistroUsuarioHandler(IUnitOfWork unitOfWork, ITokenService tokenService) 
        {
            _unitOfWork = unitOfWork;
            _tokens = tokenService;
        }
        public async Task<UsuarioDto> Handle(RegistroUsuarioCommand request, CancellationToken cancellationToken)
        {
            var req = request.Usuario;
            if (!EsEmailValido(req.Email)) throw new ArgumentException("Correo no válido");

            var exists = await _unitOfWork.Usuarios.GetByEmailAsync(req.Email, cancellationToken);
            if (exists !=null) throw new ArgumentException("correo ya Registrado");

            var user = new Usuario { Email = req.Email, Constrasena = _unitOfWork.passwordHasher.Hash(req.constrasena), Rol = req.IsAdmin ? Roles.Admin : Roles.User };
            await _unitOfWork.Usuarios.AddAsync(user, cancellationToken);
            var token = _tokens.CreateToken(user);
            var response = new UsuarioDto
            {
                Token = token,
                Email = user.Email,
                Rol = user.Rol
            };

            return response;
        }
        public bool EsEmailValido(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
