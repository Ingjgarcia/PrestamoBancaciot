using PrestamoBancario.Domain.Enums;

namespace PrestamoBancario.Application.PrestamosFeature.Dtos
{
    public class PrestamoDto
    {
        public long Id { get; set; }
        public string Usuario { get; set; } = default!;

        public decimal Cantidad { get; set; }
        public int Tiempo { get; set; }
        public EstadoPrestamo Estado { get; set; }
        public string FechaCreacion { get; set; } = default!;
        public string? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
    }
}
