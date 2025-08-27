using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Application.PrestamosFeature.Servicios;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.Prestamo.Handler
{
    internal class AprobarPrestamoHandler : IRequestHandler<AprobarPrestamoCommand, Unit>
    {
        private readonly ServicioPrestamo _service;
        private readonly ICache _cache;
        public AprobarPrestamoHandler(ServicioPrestamo service, ICache cache)
        {
            _service = service;
            _cache = cache;
        }
        public async Task<Unit> Handle(AprobarPrestamoCommand request, CancellationToken cancellationToken)
        {
            await _service.AprobarAsync(request.Id, request.AdminUser, cancellationToken);
            _cache?.Remove($"Prestamo:{request.Id}");
            return Unit.Value;

        }
    }
}
