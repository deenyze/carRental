using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarCategoryRepositoryAPI : ICarCategoryAPI
    {
        private readonly CarRentalContext context;

        public CarCategoryRepositoryAPI(CarRentalContext carContext)
        {
            context = carContext;
        }

        public async Task CreateAsync(CarCategory carCategory)
        {
            context.CarCategories.Add(carCategory);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarCategory carCategory)
        {
            context.CarCategories.Remove(carCategory);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarCategory>> GetAllAsync()
        {
            return await context.CarCategories.OrderBy(c => c.Id).ToListAsync();
        }
        public async Task<IEnumerable<CarCategory>> GetSearchedAsync(string search)
        {
            return await context.CarCategories.Where(c => c.Name.Contains(search)).OrderBy(c => c.Id).ToListAsync();
        }

        public async Task<CarCategory> GetByIdAsync(int id)
        {
            return await context.CarCategories.Include(c => c.Cars).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(CarCategory carCategory)
        {
            context.CarCategories.Update(carCategory);
            await context.SaveChangesAsync();
        }
    }
}
