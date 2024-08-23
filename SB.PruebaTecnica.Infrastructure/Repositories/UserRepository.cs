using SB.PruebaTecnica.Domain.Entities;
using SB.PruebaTecnica.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SB.PruebaTecnica.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _filePath = "Data/Users.txt";

        public async Task<User> GetByEmailAsync(string email)
        {
            var users = await GetAllUsers();
            return users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<bool> AddAsync(User user)
        {
            var users = await GetAllUsers();
            users.Add(user);
            return await SaveAllUsers(users);
        }

        private async Task<List<User>> GetAllUsers()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json = await File.ReadAllTextAsync(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<User>();
            }
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        private async Task<bool> SaveAllUsers(List<User> users)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var json = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(_filePath, json);
            return true;
        }
    }
}
