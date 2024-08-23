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
    public class AddGovernmentEntityCommandHandler : IRequestHandler<AddGovernmentEntityCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddGovernmentEntityCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddGovernmentEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = new GovernmentEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                Website = request.Website
            };

            return await _unitOfWork.GovernmentEntities.AddAsync(entity);
        }
    }
}
