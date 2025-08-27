using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts.Repository;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetByIdPrestamoHandler : IRequestHandler<GetByIdPrestamoQuery, PrestamoDto>
    {
        private readonly ICache _cache;
        private readonly IPrestamoRepository _prestamo;

        public GetByIdPrestamoHandler(ICache cache, IPrestamoRepository prestamo)
        {
            _cache = cache;
            _prestamo = prestamo;

        }
        public async Task<PrestamoDto> Handle(GetByIdPrestamoQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"prestamo:{request.Id}";
            var prestamo = await _cache.GetOrSetAsync(cacheKey, async () => await _prestamo.GetByIdAsync(request.Id, cancellationToken), TimeSpan.FromSeconds(30));
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
