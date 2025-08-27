using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;

namespace PrestamoBancario.Application.PrestamosFeature.Querys
{
    public class GetPendientesPrestamoQuery : IRequest<IEnumerable<PrestamoDto>>
    {
    }
}
