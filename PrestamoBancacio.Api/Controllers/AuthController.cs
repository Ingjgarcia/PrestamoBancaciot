using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancacio.Api.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUsuarioRepository _users;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokens;

        public AuthController(IUsuarioRepository users, IPasswordHasher hasher, ITokenService tokens)
        { _users = users; _hasher = hasher; _tokens = tokens; 
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest req, CancellationToken ct)
        {
            var exists = await _users.GetByEmailAsync(req.Email, ct);
            if (exists != null) return Conflict(new { message = "correo ya Registrado" });
            var user = new Usuario { Email = req.Email, Constrasena = _hasher.Hash(req.Password), Rol = req.IsAdmin ? Roles.Admin : Roles.User };
            await _users.AddAsync(user, ct);
            var token = _tokens.CreateToken(user);
            return Ok(new { token, email = user.Email, role = user.Rol });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
        {
            var user = await _users.GetByEmailAsync(req.Email, ct);
            if (user == null || !_hasher.Verify(req.Password, user.Constrasena))
                return Unauthorized(new { message = "Usuario o contraseña no valido" });
            var token = _tokens.CreateToken(user);
            return Ok(new { token, email = user.Email, role = user.Rol });
        }

    }
}

public record RegisterRequest(string Email, string Password, bool IsAdmin = false);
public record LoginRequest(string Email, string Password);
