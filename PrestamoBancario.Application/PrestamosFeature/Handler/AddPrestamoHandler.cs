using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Servicios;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class AddPrestamoHandler : IRequestHandler<AddPrestamoCommand, PrestamoDto>
    {
        private readonly ServicioPrestamo _service;
        public AddPrestamoHandler(ServicioPrestamo service) { _service = service; }
        public async Task<PrestamoDto> Handle(AddPrestamoCommand request, CancellationToken cancellationToken)
        {
            var prestamo = await _service.CrearAsync(request.Prestamo.IdUsuario, request.Prestamo.Cantidad, request.Prestamo.Tiempo, cancellationToken);
            var response = new PrestamoDto()
            {
                Id = prestamo.Id,
                Cantidad = prestamo.Cantidad,
                Tiempo = prestamo.Tiempo,
                Estado = prestamo.Estado,
            };

            return response;
        }
    }
}
