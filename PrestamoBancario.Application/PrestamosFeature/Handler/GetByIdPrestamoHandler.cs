using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;
using PrestamoBancario.Application.PrestamosFeature.Querys;
using PrestamoBancario.Domain.Constracts;

namespace PrestamoBancario.Application.PrestamosFeature.Handler
{
    internal class GetByIdPrestamoHandler : IRequestHandler<GetByIdPrestamoQuery, PrestamoDto>
    {
        private readonly ICache _cache;
        private readonly IUnitOfWork _unitOfWork;

        public GetByIdPrestamoHandler(ICache cache, IUnitOfWork unitOfWork)
        {
            _cache = cache;
            _unitOfWork = unitOfWork;

        }
        public async Task<PrestamoDto> Handle(GetByIdPrestamoQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"prestamo:{request.Id}";
            var prestamo = await _cache.GetOrSetAsync(cacheKey, async () => await _unitOfWork.Prestamos.GetByIdAsync(request.Id, cancellationToken), TimeSpan.FromSeconds(30)) ?? throw new KeyNotFoundException("Prestamo no encontrado");


            var response = new PrestamoDto()
            {
                Id = prestamo.Id,
                Cantidad = prestamo.Cantidad,
                Tiempo = prestamo.Tiempo,
                Estado = prestamo.Estado,
                //IdUsuario = usuario?.Email??"",
                FechaCreacion = prestamo.FechaCreacion.ToShortDateString(),
                FechaModificacion = prestamo.FechaModificacion?.ToShortDateString(),
                //UsuarioModificacion = admin?.Email
            };

            return response;
        }
    }
}
