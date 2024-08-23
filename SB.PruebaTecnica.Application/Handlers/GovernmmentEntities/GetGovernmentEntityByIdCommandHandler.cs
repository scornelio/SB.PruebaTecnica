using MediatR;
using SB.PruebaTecnica.Application.Commands.GovernmentEntities;
using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Application.Handlers.GovernmmentEntities
{
    public class GetGovernmentEntityByIdCommandHandler : IRequestHandler<GetGovernmentEntityByIdCommand, GovernmentEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetGovernmentEntityByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GovernmentEntity> Handle(GetGovernmentEntityByIdCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GovernmentEntities.GetByIdAsync(request.Id);
        }
    }
}
