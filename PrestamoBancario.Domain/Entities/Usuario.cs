namespace PrestamoBancario.Domain.Entities
{
    internal class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Email { get; set; }
        public string Constrasena{ get; set; } = default!;
        public string Rol { get; set; } = Roles.User; 

        public bool Estado { get; set; }

    }
}
