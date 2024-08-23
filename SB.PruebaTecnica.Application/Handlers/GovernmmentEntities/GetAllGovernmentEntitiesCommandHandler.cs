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
    public class GetAllGovernmentEntitiesCommandHandler : IRequestHandler<GetAllGovernmentEntitiesCommand, List<GovernmentEntity>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllGovernmentEntitiesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GovernmentEntity>> Handle(GetAllGovernmentEntitiesCommand request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GovernmentEntities.GetAllEntities();
        }
    }
}
