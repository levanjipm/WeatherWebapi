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
            new Country { Id = 3, Name = "Australia" },
            new Country { Id = 4, Name = "Indonesia" }
            // Add more countries...
        };

        private static readonly Dictionary<int, List<City>> citiesByCountry = new Dictionary<int, List<City>>
        {
            { 1, new List<City> { new City { Id = 1, Name = "New York" }, new City { Id = 2, Name = "Los Angeles" } } },
            { 2, new List<City> { new City { Id = 3, Name = "Toronto" }, new City { Id = 4, Name = "Vancouver" } } },
            { 3, new List<City> { new City { Id = 5, Name = "Sydney" }, new City { Id = 6, Name = "Brisbane" } } },
            { 4, new List<City> { new City { Id = 7, Name = "Jakarta" }, new City { Id = 8, Name = "Bandung" } } }
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
