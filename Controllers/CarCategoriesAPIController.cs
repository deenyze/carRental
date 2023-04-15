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
    public class CarCategoriesAPIController : ControllerBase
    {
        private readonly ICarCategoryAPI carCatRep;

        public CarCategoriesAPIController(ICarCategoryAPI carCatRep)
        {
            this.carCatRep = carCatRep;
        }

        // GET: api/CarCategories
        [HttpGet]
        public async Task<IEnumerable<CarCategory>> GetCategories()
        {
            return await carCatRep.GetAllAsync();
        }

        // GET: api/CarCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarCategory>> GetCarCategory(int id)
        {
            var carCategory = await carCatRep.GetByIdAsync(id);

            if (carCategory == null)
            {
                return NotFound();
            }

            return carCategory;
        }

        // PUT: api/CarCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarCategory(int id, [FromBody] CarCategory carCategory)
        {
            if (id != carCategory.Id)
            {
                return BadRequest();
            }

            //_context.Entry(carCategory).State = EntityState.Modified;

            try
            {
                await carCatRep.UpdateAsync(carCategory);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarCategoryExists(id))
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

        // POST: api/CarCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarCategory>> PostCarCategory(CarCategory carCategory)
        {
            await carCatRep.CreateAsync(carCategory);

            return CreatedAtAction("GetCarCategory", new { id = carCategory.Id }, carCategory);
        }

        // DELETE: api/CarCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarCategory(int id)
        {
            var carCategory = await carCatRep.GetByIdAsync(id);
            if (carCategory == null)
            {
                return NotFound();
            }

            await carCatRep.DeleteAsync(carCategory);

            return NoContent();
        }

        private bool CarCategoryExists(int id)
        {
            var carCategory = carCatRep.GetByIdAsync(id);
            if (carCategory != null)
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
