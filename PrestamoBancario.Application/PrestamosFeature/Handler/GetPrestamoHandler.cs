using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetPrestamoHandler : IRequestHandler<GetPrestamoQuery, IEnumerable<PrestamoDto>>
    {
        private readonly ICache _cache;
        private readonly IUnitOfWork _unitOfWork;

        public GetPrestamoHandler(ICache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;

        }
        public async Task<IEnumerable<PrestamoDto>> Handle(GetPrestamoQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"prestamo:usuario:{request.IdUsuario}";
            var user = await _unitOfWork.Usuarios.GetByIdAsync(request.IdUsuario, cancellationToken);
            var prestamos = await _cache.GetOrSetAsync(
                                    cacheKey,
                                    async () =>
                                    {
                                        return user?.Rol == "Admin"
                                            ? await _unitOfWork.Prestamos.GetAllAsync(cancellationToken)
                                            : await _unitOfWork.Prestamos.GetByUserAsync(request.IdUsuario, cancellationToken);
                                    },
                                    TimeSpan.FromSeconds(30)
                                );

            var response = prestamos?.Select(x => new PrestamoDto
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                Tiempo = x.Tiempo,
                Estado = x.Estado,
                IdUsuario = x.IdUsuario,
            }).ToList();

            return response ?? [];
        }
    }
}
