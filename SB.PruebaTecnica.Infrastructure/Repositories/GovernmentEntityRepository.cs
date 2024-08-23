using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;
using System.Text.Json;


namespace SB.PruebaTecnica.Infrastructure.Repositories
{
    public class GovernmentEntityRepository : IGovernmentEntityRepository
    {
        private readonly string _filePath = "Data/GovernmentEntities.txt";

        public async Task<bool> AddAsync(GovernmentEntity entity)
        {
            var entities = await GetAllEntities();
            entities.Add(entity);
            return await SaveAllEntities(entities);
        }

        public async Task<List<GovernmentEntity>> GetAllEntities()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                return new List<GovernmentEntity>();
            }

            var json = await File.ReadAllTextAsync(_filePath);
            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<GovernmentEntity>();
            }
            return JsonSerializer.Deserialize<List<GovernmentEntity>>(json);
        }

        private async Task<bool> SaveAllEntities(List<GovernmentEntity> entities)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(entities);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }

        public async Task<GovernmentEntity> GetByIdAsync(Guid id)
        {
            var entities = await GetAllEntities();
            return entities.FirstOrDefault(e => e.Id == id);
        }

        public async Task<bool> UpdateAsync(GovernmentEntity updatedEntity)
        {
            var entities = await GetAllEntities();
            var existingEntity = entities.FirstOrDefault(e => e.Id == updatedEntity.Id);

            if (existingEntity != null)
            {
                entities.Remove(existingEntity);
                entities.Add(updatedEntity);
                return await SaveAllEntities(entities);
            }

            return false;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entities = await GetAllEntities();
            var entityToDelete = entities.FirstOrDefault(e => e.Id == id);

            if (entityToDelete != null)
            {
                entities.Remove(entityToDelete);
                return await SaveAllEntities(entities);
            }

            return false;
        }
    }
}
