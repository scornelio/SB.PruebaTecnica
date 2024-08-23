using MediatR;
using SB.PruebaTecnica.Domain.Entities;

namespace SB.PruebaTecnica.Application.Commands.GovernmentEntities
{
    public class UpdateGovernmentEntityCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }
}
