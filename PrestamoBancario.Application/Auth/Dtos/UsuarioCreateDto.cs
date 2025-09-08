namespace PrestamoBancario.Application.Auth.Dtos
{
    public class UsuarioCreateDto
    {
        public string Email { get; set; } = default!;
        public string constrasena { get; set; } = default!;
        public bool IsAdmin { get; set; } = false;
    }
}
