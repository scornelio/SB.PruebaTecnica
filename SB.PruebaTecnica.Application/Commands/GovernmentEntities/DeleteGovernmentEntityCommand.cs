using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Application.Commands.GovernmentEntities
{
    public class DeleteGovernmentEntityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
