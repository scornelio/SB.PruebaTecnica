using SB.PruebaTecnica.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepository Users { get; private set; }
        public IGovernmentEntityRepository GovernmentEntities { get; private set; }

        public UnitOfWork(IUserRepository userRepository, IGovernmentEntityRepository governmentEntityRepository)
        {
            Users = userRepository;
            GovernmentEntities = governmentEntityRepository;
        }

        public Task<int> Commit()
        {
            return Task.FromResult(1);
        }

        public void Dispose()
        {
        }
    }
}
