using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrestamoBancario.Application.Auth.Command;
using PrestamoBancario.Application.Auth.Dtos;
using PrestamoBancario.Application.Auth.Querys;

namespace PrestamoBancacio.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
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
            return await _mediator.Send(new RegistroUsuarioCommand() { Usuario = req });
        }

        [HttpPost("login")]
        public async Task<UsuarioDto> Login([FromBody] UsuarioLoginDto req, CancellationToken ct)
        {
            return await _mediator.Send(new LoginQuery() { Usuario = req });
        }

        [Authorize]
        [HttpGet("claims")]
        public IActionResult GetClaims()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(claims);
        }

    }
}

