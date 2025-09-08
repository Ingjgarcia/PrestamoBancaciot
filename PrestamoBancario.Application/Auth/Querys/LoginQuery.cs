using MediatR;
using PrestamoBancario.Application.Auth.Dtos;

namespace PrestamoBancario.Application.Auth.Querys
{
    public class LoginQuery : IRequest<UsuarioDto>
    {
        public UsuarioLoginDto Usuario { get; set; } = default!;
    }
}
