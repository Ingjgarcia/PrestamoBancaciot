using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrestamoBancario.Application.Auth.Command;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Application.Auth.Querys;

namespace PrestamoBancacio.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<UsuarioDto> Register([FromBody] UsuarioCreateDto req, CancellationToken ct)
        {
            var exists = await _mediator.Send(new GetUsuarioCommand() { email = req.Email });

            if (exists) throw new ArgumentException( "correo ya Registrado");
           
            return await _mediator.Send(new RegistroUsuarioCommand() { Usuario = req });
        }

        [HttpPost("login")]
        public async Task<UsuarioDto> Login([FromBody] UsuarioLoginDto req, CancellationToken ct)
        {
            var user = await _mediator.Send(new LoginQuery() { Usuario = req });

           
            if (user == null)
                throw new  AccessViolationException("Usuario o contraseña no valido");

            return user;
        }

    }
}

