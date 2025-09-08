using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;

namespace PrestamoBancario.Application.PrestamosFeature.Querys
{
    public class GetPrestamoQuery : IRequest<IEnumerable<PrestamoDto>>
    {
        public int IdUsuario { get; set; }
    }
}
