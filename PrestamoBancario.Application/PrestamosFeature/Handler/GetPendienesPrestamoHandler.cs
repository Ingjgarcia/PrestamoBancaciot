using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetPendienesPrestamoHandler : IRequestHandler<GetPendientesPrestamoQuery, IEnumerable<PrestamoDto>>
    {
        private readonly IPrestamoRepository _prestamo;

        public GetPendienesPrestamoHandler(IPrestamoRepository prestamo)
        {
            _prestamo = prestamo;

        }
        public async Task<IEnumerable<PrestamoDto>> Handle(GetPendientesPrestamoQuery request, CancellationToken cancellationToken)
        {
            var prestamos = await _prestamo.GetPendingAsync(cancellationToken);
            var response = prestamos.Select(x => new PrestamoDto
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                Tiempo = x.Tiempo,
                Estado = x.Estado
            }).ToList();

            return response;
        }
    }
}
