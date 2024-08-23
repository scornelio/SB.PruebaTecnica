using MediatR;
using SB.PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Application.Commands.GovernmentEntities
{
    public class GetAllGovernmentEntitiesCommand : IRequest<List<GovernmentEntity>>
    {
    }
}
