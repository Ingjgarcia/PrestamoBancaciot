using MediatR;
using PrestamoBancario.Application.Command;
using PrestamoBancario.Application.Servicios;
using PrestamoBancario.Domain.Constracts.Repository;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Application.Handler
{
    internal class AddPrestamoHandler : IRequestHandler<AddPrestamoCommand, Prestamo>
    {
        private readonly ServicioPrestamo _service;
        public AddPrestamoHandler(ServicioPrestamo service) { _service = service; }
        public async Task<Prestamo> Handle(AddPrestamoCommand request, CancellationToken cancellationToken)
        {
            var prestamo = await _service.CrearAsync(request.prestamo.idUsuario, request.prestamo.Cantidad, request.prestamo.Tiempo, cancellationToken);
            return prestamo;
        }
    }
}
