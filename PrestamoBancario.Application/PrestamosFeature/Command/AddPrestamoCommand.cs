using MediatR;
using PrestamoBancario.Application.PrestamosFeature.Dtos;

namespace PrestamoBancario.Application.PrestamosFeature.Command
{
    public class AddPrestamoCommand : IRequest<PrestamoDto>
    {
        public PrestamoCreateDto Prestamo { get; set; } = default!;
    }
}
