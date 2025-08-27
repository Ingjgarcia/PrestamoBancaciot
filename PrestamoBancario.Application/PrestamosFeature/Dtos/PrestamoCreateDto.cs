namespace PrestamoBancario.Application.PrestamosFeature.Dtos
{
    public class PrestamoCreateDto
    {
        public Guid IdUsuario { get; set; }
        public decimal Cantidad { get; set; }
        public int Tiempo { get; set; }
    }
}
