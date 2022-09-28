using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly DataContext _context;

        public CityController(DataContext context)
        {
            _context = context;
        }

        // Get all countries
        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get()
        {

            return Ok(await _context.Countries.ToListAsync());
        }

        // Get a specific country by id
        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> Get(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
                return BadRequest("Couldn't find that country");

            return Ok(country);
        }

        // Add a country
        [HttpPost]
        public async Task<ActionResult<List<Country>>> AddCountry(Country country)
        {
            // add the change
            _context.Countries.Add(country);
            // save the change
            await _context.SaveChangesAsync();
            return Ok(await _context.Countries.ToListAsync());
        }

        // Update a country
        [HttpPut]
        public async Task<ActionResult<List<Country>>> UpdateCountry(Country countryRequest)
        {

            var dbcities = await _context.Countries.FindAsync(countryRequest.ID);
            if (dbcities == null)
            {
                return BadRequest("Couldn't find that country");
            }

            dbcities.Name = countryRequest.Name;
            dbcities.Capital = countryRequest.Capital;
            dbcities.Language = countryRequest.Language;
            dbcities.Flower = countryRequest.Flower;

            // save the changes
            await _context.SaveChangesAsync();
            //return all of the countries
            return Ok(await _context.Countries.ToListAsync());
        }

        // Delete a country
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Country>>> Delete(int id)
        {
            var dbcountries = await _context.Countries.FindAsync(id);
            if (dbcountries == null)
                return BadRequest("Couldn't find that country");

            _context.Countries.Remove(dbcountries);
            // save the changes
            await _context.SaveChangesAsync();
            //return all of the countries
            return Ok(await _context.Countries.ToListAsync());
        }

    }
}
