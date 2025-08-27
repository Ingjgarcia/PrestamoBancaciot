using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Application.PrestamosFeature.Servicios;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class RechazarPrestamoHandler : IRequestHandler<RechazarPrestamoCommand, Unit>
    {
        private readonly ServicioPrestamo _service;
        private readonly ICache _cache;
        public RechazarPrestamoHandler(ServicioPrestamo service, ICache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<Unit> Handle(RechazarPrestamoCommand request, CancellationToken cancellationToken)
        {
            await _service.RechazarAsync(request.Id, request.AdminUser, cancellationToken);
            _cache?.Remove($"Prestamo:{request.Id}");
            return Unit.Value;

        }
    }
}
