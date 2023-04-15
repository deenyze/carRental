using CarRentalAPI.Models;

namespace CarRentalAPI.Data
{
    public interface ICarCategoryAPI
    {
        Task CreateAsync(CarCategory carCategory);
        Task DeleteAsync(CarCategory carCategory);
        Task<IEnumerable<CarCategory>> GetAllAsync();
        Task<CarCategory> GetByIdAsync(int id);
        Task<IEnumerable<CarCategory>> GetSearchedAsync(string search);
        Task UpdateAsync(CarCategory carCategory);
    }
}