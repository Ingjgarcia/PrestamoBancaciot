using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Command;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Domain.Enums;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class AddPrestamoHandler : IRequestHandler<AddPrestamoCommand, PrestamoDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddPrestamoHandler(IUnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<PrestamoDto> Handle(AddPrestamoCommand request, CancellationToken cancellationToken)
        {
            var req = request.Prestamo;

            if (req.Cantidad <= 0) throw new ArgumentException("Cantidad debe ser mayor que cero");

            if (req.Tiempo <= 0) throw new ArgumentException("el tiempo debe ser mayor a cero");

            var prestamo = new Domain.Entities.Prestamo { IdUsuario = req.IdUsuario, Cantidad = req.Cantidad, Tiempo = req.Tiempo, Estado = EstadoPrestamo.pendiente };
            try
            {
                await _unitOfWork.Prestamos.AddAsync(prestamo, cancellationToken);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

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
