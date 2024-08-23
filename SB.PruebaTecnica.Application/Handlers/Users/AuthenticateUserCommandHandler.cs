using MediatR;
using SB.PruebaTecnica.Application.Commands.Users;
using SB.PruebaTecnica.Application.DTOs.Response;
using SB.PruebaTecnica.Application.Interfaces;
using SB.PruebaTecnica.Domain.Interfaces;

namespace SB.PruebaTecnica.Application.Handlers.Users
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, LoginResponse>
    {
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateUserCommandHandler(IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            var loginResponse = new LoginResponse
            {
                Token = _tokenService.GenerateToken(user)
            };

            return loginResponse;
        }
    }
}
