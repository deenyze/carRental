using CarRentalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class CarRepositoryAPI : ICarAPI
    {
        private readonly CarRentalContext context;

        public CarRepositoryAPI(CarRentalContext carContext)
        {
            context = carContext;
        }

        public async Task CreateAsync(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Task<Car> car = GetByIdAsync(id);

            if(car != null)
            {
                await context.Cars.async(car);
            }
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await context.Cars.OrderBy(c => c.CarId).ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await context.Cars.FirstOrDefaultAsync(c => c.CarId == id);
        }

        public async Task UpdateAsync(Car car)
        {
            context.Update(car);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Car>> SearchCarAsync(string search)
        {
            return await context.Cars.Where
                (c => c.Model.Contains(search) || c.Brand.Contains(search)).ToListAsync();
        }
    }
}
