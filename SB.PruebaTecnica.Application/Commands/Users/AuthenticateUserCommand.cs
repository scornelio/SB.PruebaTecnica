using MediatR;
using SB.PruebaTecnica.Application.DTOs.Response;

namespace SB.PruebaTecnica.Application.Commands.Users
{
    public class AuthenticateUserCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
