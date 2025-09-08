using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;

namespace PrestamoBancario.Application.PrestamosFeature.Querys
{
    public class GetByIdPrestamoQuery : IRequest<PrestamoDto>
    {
        public long Id { get; set; }
    }
}
