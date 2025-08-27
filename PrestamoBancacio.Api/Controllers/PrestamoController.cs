using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrestamoBancario.Application.Dtos;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Infrastructure.Cache;

namespace PrestamoBancacio.Api.Controllers
{
    public class PrestamoController : Controller
    {
        private readonly ServicioPrestamo _service;
        private readonly IPrestamoRepository _prestamos;
        private readonly IUsuarioRepository _users;
        private readonly ICache _cache;


        private Guid GetUserId() => Guid.Parse(User.FindFirst("sub")!.Value);
        private string GetEmail() => User.FindFirst("email")!.Value;

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PrestamoDto req, CancellationToken ct)
        {
            var userId = GetUserId();
            var prestamo = await _service.CreateAsync(userId, req.Cantidad, req.Tiempo, ct);
            return CreatedAtAction(nameof(GetById), new { id = prestamo.Id }, prestamo);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id, CancellationToken ct)
        {
            var cacheKey = $"prestamo:{id}";
            var prestamo = await _cache.GetOrSetAsync(cacheKey, async () => await _prestamos.GetByIdAsync(id, ct), TimeSpan.FromSeconds(30));
            if (prestamo == null) return NotFound(new { message = "Prestamo" });
            if (User.IsInRole("User") && prestamo.IdUsuario != GetUserId()) return Forbid();
            return Ok(prestamo);
        }

        [Authorize]
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll(CancellationToken ct)
        {
            var userId = GetUserId();
            var cacheKey = $"prestamo:usuario:{userId}";
            var prestamos = await _cache.GetOrSetAsync(cacheKey, async () => await _prestamos.GetByUserAsync(userId, ct), TimeSpan.FromSeconds(15));
            return Ok(prestamos);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("prestamos-pendientes")]
        public async Task<ActionResult> Pendientes(CancellationToken ct)
        {
            var prestamos = await _prestamos.GetPendingAsync(ct);
            return Ok(prestamos);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("{id:guid}/approvar")]
        public async Task<IActionResult> Approvar(Guid id, CancellationToken ct)
        {
            await _service.ApproveAsync(id, GetEmail(), ct);
            _cache.Remove($"Prestamo:{id}");
            return NoContent();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("{id:guid}/rechazar")]
        public async Task<IActionResult> Reject(Guid id, CancellationToken ct)
        {
            await _service.RejectAsync(id, GetEmail(), ct);
            _cache.Remove($"loan:{id}");
            return NoContent();
        }
    }
}

