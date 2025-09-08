using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;

namespace PrestamoBancacio.Api.Controllers
{
    [Route("api/prestamos")]
    [ApiController]
    [Authorize]

    public class PrestamoController : Controller
    {
        private readonly IMediator _mediator;

        public PrestamoController (IMediator mediator) => _mediator = mediator;
        private int GetUserId() => int.Parse(User.FindFirst("Userid")!.Value);

        [HttpPost("Add")]
        public async Task<PrestamoDto> Add([FromBody] PrestamoCreateDto req, CancellationToken ct)
        {
            req.IdUsuario = GetUserId();
            return await _mediator.Send(new AddPrestamoCommand() { Prestamo = req });
        }

        [HttpGet("{id:long}")]
        public async Task<PrestamoDto> GetById(long id, CancellationToken ct)
        {
            var prestamo = await _mediator.Send(new GetByIdPrestamoQuery { Id = id }, ct);

            //if (User.IsInRole("User") && prestamo.IdUsuario != GetUserId())
            //    throw new AccessViolationException("no tiene permiso para esta accion");

            return prestamo;
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<PrestamoDto>> GetAll(CancellationToken ct)
        {
            return await _mediator.Send(new GetPrestamoQuery() { IdUsuario = GetUserId() });
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("prestamos-pendientes")]
        public async Task<IEnumerable<PrestamoDto>> Pendientes(CancellationToken ct)
        {
            return await _mediator.Send(new GetPendientesPrestamoQuery());
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("{id:long}/approbar")]
        public async Task Approbar(long id, CancellationToken ct)
        {
            await _mediator.Send(new AprobarPrestamoCommand { Id = id, AdminUser = GetUserId() }, ct);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("{id:long}/rechazar")]
        public async Task Rechazar(long id, CancellationToken ct)
        {
            await _mediator.Send(new RechazarPrestamoCommand { Id = id, AdminUser = GetUserId() }, ct);

        }
    }
}

