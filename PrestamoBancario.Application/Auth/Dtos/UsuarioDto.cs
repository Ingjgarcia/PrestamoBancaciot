namespace PrestamoBancario.Application.Auth.Dtos
{
    public class UsuarioDto
    {
        public string Email { get; set; }
        public string? Token { get; set; } 
        public string Rol { get; set; } 
    }
}
