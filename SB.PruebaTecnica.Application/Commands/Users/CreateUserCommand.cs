using MediatR;
using SB.PruebaTecnica.Application.DTOs.Request;

namespace SB.PruebaTecnica.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
    }
}
