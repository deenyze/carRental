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
    public class CarsAPIController : ControllerBase
    {
        private readonly ICarAPI carRep;
        private readonly ICarCategoryAPI carCategoryRep;

        public CarsAPIController(ICarAPI carRep, ICarCategoryAPI carCategoryRep)
        {
            this.carRep = carRep;
            this.carCategoryRep = carCategoryRep;
        }

        // GET: api/Cars
        [HttpGet]
        public async Task<IEnumerable<Car>> GetCars()
        {
            return await carRep.GetAllAsync();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await carRep.GetByIdAsync(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // PUT: api/Cars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCar(int id, [FromBody] Car car)
        {
            if (id != car.CarId)
            {
                return BadRequest();
            }

            //vad gör denna?
            //_context.Entry(car).State = EntityState.Modified;

            try
            {
                await carRep.UpdateAsync(car);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
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

        // POST: api/Cars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            await carRep.CreateAsync(car);

            return CreatedAtAction("GetCar", new { id = car.CarId }, car);
        }

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            if (carRep.GetByIdAsync(id) == null)
            {
                return NotFound();
            }

            await carRep.DeleteAsync(id, car);

            return NoContent();
        }

        private bool CarExists(int id)
        {
            var car = carRep.GetByIdAsync(id);
            if (car != null)
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
