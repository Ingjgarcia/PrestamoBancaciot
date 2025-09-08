namespace PrestamoBancario.Domain.Entities
{
    internal class Usuario
    {
        public long Id { get; set; }
        public string Email { get; set; } = default!;
        public string Constrasena{ get; set; } = default!;
        public string Rol { get; set; } = Roles.User; 

        public bool Estado { get; set; }

        public virtual ICollection<Prestamo> PrestamosCreados { get; set; } = [];
        public virtual ICollection<Prestamo> PrestamosModificados { get; set; } = [];


    }
}
