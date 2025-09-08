namespace PrestamoBancario.Application.PrestamosFeature.Dtos
{
    public class PrestamoCreateDto
    {
        public int IdUsuario { get; set; }
        public decimal Cantidad { get; set; }
        public int Tiempo { get; set; }
    }
}
