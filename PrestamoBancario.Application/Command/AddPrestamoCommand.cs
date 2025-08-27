using MediatR;
using PrestamoBancario.Application.Dtos;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Application.Command
{
    public class AddPrestamoCommand: IRequest<Prestamo>
    {
        public required PrestamoDto prestamo {  get; set; }
    }
}
