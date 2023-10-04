using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeatherWebapi.Models;

namespace WeatherWebapi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private static readonly List<Country> countries = new List<Country>
    {
        new Country { Id = 1, Name = "USA" },
        new Country { Id = 2, Name = "Canada" },
        // Add more countries...
    };

        private static readonly Dictionary<int, List<City>> citiesByCountry = new Dictionary<int, List<City>>
    {
        { 1, new List<City> { new City { Id = 1, Name = "New York" }, new City { Id = 2, Name = "Los Angeles" } } },
        { 2, new List<City> { new City { Id = 3, Name = "Toronto" }, new City { Id = 4, Name = "Vancouver" } } },
        // Add more cities...
    };

        [HttpGet("countries")]
        public ActionResult<IEnumerable<Country>> GetCountries()
        {
            return Ok(countries);
        }

        [HttpGet("cities/{countryId}")]
        public ActionResult<IEnumerable<City>> GetCities(int countryId)
        {
            if (citiesByCountry.TryGetValue(countryId, out var cities))
            {
                return Ok(cities);
            }
            return NotFound();
        }
    }
}
