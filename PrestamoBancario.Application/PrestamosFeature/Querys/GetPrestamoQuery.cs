using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;

namespace PrestamoBancario.Application.PrestamosFeature.Querys
{
    public class GetPrestamoQuery : IRequest<IEnumerable<PrestamoDto>>
    {
        public Guid IdUsuario { get; set; }
    }
}
