using MediatR;
using SB.PruebaTecnica.Application.Commands.GovernmentEntities;
using SB.PruebaTecnica.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Application.Handlers.GovernmmentEntities
{
    public class DeleteGovernmentEntityCommandHandler : IRequestHandler<DeleteGovernmentEntityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteGovernmentEntityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteGovernmentEntityCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GovernmentEntities.DeleteAsync(request.Id);
        }
    }
}
