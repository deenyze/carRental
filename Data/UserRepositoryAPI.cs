using CarRentalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAPI.Data
{
    public class UserRepositoryAPI : IUserAPI
    {
        private readonly CarRentalContext context;

        public UserRepositoryAPI(CarRentalContext carContext)
        {
            context = carContext;
        }
        public async Task AddAsync(User user)
        {
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int? id)
        {
            var userToDelete = await GetByIdAsync(id);
            if (userToDelete != null)
            {
                context.Users.Remove(userToDelete);
                await context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.OrderBy(x => x.UserId).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetSearchedAsync(string search)
        {
            return await context.Users.Where(x => x.UserName.Contains(search))
                .OrderBy(x => x.UserId).ToListAsync();
        }
        public async Task<User> GetByIdAsync(int? id)
        {
            var tempUser = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return tempUser;
        }
        public async Task UpdateAsync(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
