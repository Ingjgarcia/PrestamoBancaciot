using MediatR;
using PrestamoBancario.Application.Auth.Dtos;

namespace PrestamoBancario.Application.Auth.Command
{
    public class RegistroUsuarioCommand : IRequest<UsuarioDto>
    {
        public UsuarioCreateDto Usuario { get; set; }
    }
}
