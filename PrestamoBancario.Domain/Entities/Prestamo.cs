using PrestamoBancario.Domain.Enums;

namespace PrestamoBancario.Domain.Entities
{
    internal class Prestamo
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public decimal Cantidad { get; set; }
        public int Tiempo { get; set; }
        public EstadoPrestamo Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }
        public long?  IdUsuarioModificacion { get; set; }
        public virtual Usuario UsuarioCreacion { get; set; } = default!;
        public virtual Usuario? UsuarioModificacion { get; set; }

        public void Aprobar(long adminUser)
        {
            if (Estado != EstadoPrestamo.pendiente)
                throw new InvalidOperationException("Slo prestamos pendientes pueden ser aprobados.");
            Estado = EstadoPrestamo.Aprobado;
            FechaModificacion = DateTime.UtcNow;
            IdUsuarioModificacion = adminUser;
        }

        public void Rechazar(long adminUser)
        {
            if (Estado != EstadoPrestamo.pendiente)
                throw new InvalidOperationException("solo prestamos pendientes pueden ser Denegados.");
            Estado = EstadoPrestamo.Rechazado;
            FechaModificacion = DateTime.UtcNow;
            IdUsuarioModificacion = adminUser;
        }
    }
}
