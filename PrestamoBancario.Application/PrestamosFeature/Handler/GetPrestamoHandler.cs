using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetPrestamoHandler : IRequestHandler<GetPrestamoQuery, IEnumerable<PrestamoDto>>
    {
        private readonly ICache _cache;
        private readonly IPrestamoRepository _prestamo;

        public GetPrestamoHandler(ICache cache, IPrestamoRepository prestamo)
        {
            _cache = cache;
            _prestamo = prestamo;

        }
        public async Task<IEnumerable<PrestamoDto>> Handle(GetPrestamoQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"prestamo:usuario:{request.IdUsuario}";
            var prestamos = await _cache.GetOrSetAsync(cacheKey, async () => await _prestamo.GetByUserAsync(request.IdUsuario, cancellationToken), TimeSpan.FromSeconds(30));
            var response = prestamos?.Select(x => new PrestamoDto
            {
                Id = x.Id,
                Cantidad = x.Cantidad,
                Tiempo = x.Tiempo,
                Estado = x.Estado
            }).ToList();

            return response ?? [];
        }
    }
}
