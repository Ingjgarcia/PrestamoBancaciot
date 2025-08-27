namespace PrestamoBancario.Application.Auth.Dtos
{
    public class UsuarioCreateDto
    {
        public string Email { get; set; }
        public string constrasena { get; set; } 
        public bool IsAdmin { get; set; } = false;
    }
}
