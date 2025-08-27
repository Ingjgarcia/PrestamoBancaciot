using PrestamoBancario.Domain.Enums;

namespace PrestamoBancario.Domain.Entities
{
    internal class Prestamo
    {
        public Guid Id { get; set; }
        public Guid IdUsuario { get; set; }
        public decimal Cantida { get; set; }
        public int Tiempo { get; set; }
        public EstadoPrestamo Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string  UsuarioModificacion { get; set; }

        public Usuario Usuario { get; set; }

        public void Aprobar(string adminUser)
        {
            if (Estado != EstadoPrestamo.pendiente)
                throw new InvalidOperationException("Slo prestamos pendientes pueden ser aprobados.");
            Estado = EstadoPrestamo.Aprobado;
            FechaModificacion = DateTime.UtcNow;
            UsuarioModificacion = adminUser;
        }

        public void Rechazar(string adminUser)
        {
            if (Estado != EstadoPrestamo.pendiente)
                throw new InvalidOperationException("solo prestamos pendientes pueden ser Denegados.");
            Estado = EstadoPrestamo.Rechazado;
            FechaModificacion = DateTime.UtcNow;
            UsuarioModificacion = adminUser;
        }
    }
}
