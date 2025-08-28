using MediatR;

namespace PrestamoBancario.Application.PrestamosFeature.Command
{
    public class AprobarPrestamoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public Guid AdminUser { get; set; }
    }
}
