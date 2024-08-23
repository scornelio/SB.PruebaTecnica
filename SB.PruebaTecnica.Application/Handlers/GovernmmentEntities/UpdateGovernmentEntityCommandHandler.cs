using MediatR;
using SB.PruebaTecnica.Application.Commands.GovernmentEntities;
using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;

namespace SB.PruebaTecnica.Application.Handlers.GovernmmentEntities
{
    public class UpdateGovernmentEntityCommandHandler : IRequestHandler<UpdateGovernmentEntityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGovernmentEntityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateGovernmentEntityCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GovernmentEntities.UpdateAsync(new GovernmentEntity(){Id = request.Id,
                                                                                           Name = request.Name,
                                                                                           Address = request.Address,
                                                                                           PhoneNumber = request.PhoneNumber,
                                                                                           Website = request.Website
                                                                                           });
        }
    }
}
