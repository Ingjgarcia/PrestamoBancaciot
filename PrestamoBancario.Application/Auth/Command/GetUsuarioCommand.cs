using MediatR;

namespace PrestamoBancario.Application.Auth.Command
{
    public class GetUsuarioCommand : IRequest<bool>
    {
        public string email { get; set; }
    }
}
