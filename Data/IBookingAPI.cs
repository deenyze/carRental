using CarRentalAPI.Models;

namespace CarRentalAPI.Data
{
    public interface IBookingAPI
    {
        Task AddAsync(Booking booking);
        Task DeleteAsync(int id);
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking> GetByIdAsync(int id);
        Task UpdateAsync(Booking booking);
    }
}