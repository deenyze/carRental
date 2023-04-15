using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalAPI.Data;
using CarRentalAPI.Models;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsAPIController : ControllerBase
    {
        private readonly IBookingAPI bookingRep;

        public BookingsAPIController(IBookingAPI bookingRep)
        {
            this.bookingRep = bookingRep;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<IEnumerable<Booking>> GetBookings()
        {
          
            return await bookingRep.GetAllAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await bookingRep.GetByIdAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, [FromBody] Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            //vad är skillnaden på denna?
            //context.Entry(booking).State = EntityState.Modified;

            try
            {
                await bookingRep.UpdateAsync(booking);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            await bookingRep.AddAsync(booking);

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await bookingRep.GetByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            await bookingRep.DeleteAsync(booking.Id);

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            var booking = bookingRep.GetByIdAsync(id);
            if (booking != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
