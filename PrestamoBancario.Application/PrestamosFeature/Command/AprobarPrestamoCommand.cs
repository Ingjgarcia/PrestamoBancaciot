using MediatR;

namespace PrestamoBancario.Application.PrestamosFeature.Command
{
    public class AprobarPrestamoCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public long AdminUser { get; set; }
    }
}
