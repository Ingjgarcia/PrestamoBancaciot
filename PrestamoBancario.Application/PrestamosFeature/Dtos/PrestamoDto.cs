using PrestamoBancario.Domain.Enums;

namespace PrestamoBancario.Application.PrestamosFeature.Dtos
{
    public class PrestamoDto
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public decimal Cantidad { get; set; }
        public int Tiempo { get; set; }
        public EstadoPrestamo Estado { get; set; }
    }
}
