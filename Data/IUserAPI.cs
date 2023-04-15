using CarRentalAPI.Models;

namespace CarRentalAPI.Data
{
    public interface IUserAPI
    {
        Task AddAsync(User user);
        Task DeleteAsync(int? id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int? id);
        Task<IEnumerable<User>> GetSearchedAsync(string search);
        Task UpdateAsync(User user);
    }
}