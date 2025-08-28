using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetPendienesPrestamoHandler : IRequestHandler<GetPendientesPrestamoQuery, IEnumerable<PrestamoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPendienesPrestamoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<PrestamoDto>> Handle(GetPendientesPrestamoQuery request, CancellationToken cancellationToken)
        {
            var prestamos = await _unitOfWork.Prestamos.GetPendingAsync(cancellationToken);
            var response = prestamos.Select(x => new PrestamoDto
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                Tiempo = x.Tiempo,
                Estado = x.Estado,
                IdUsuario = x.IdUsuario
            }).ToList();

            return response;
        }
    }
}
