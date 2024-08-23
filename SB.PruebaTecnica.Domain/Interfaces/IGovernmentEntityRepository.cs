using SB.PruebaTecnica.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Domain.Interfaces
{
    public interface IGovernmentEntityRepository
    {
        Task<bool> AddAsync(GovernmentEntity entity);
        Task<List<GovernmentEntity>> GetAllEntities();
        Task<GovernmentEntity> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(GovernmentEntity updatedEntity);
        Task<bool> DeleteAsync(Guid id);
    }
}
