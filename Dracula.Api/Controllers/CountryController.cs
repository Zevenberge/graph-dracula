using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dracula.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Dracula.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countries;
        public CountryController(ICountryRepository countries)
        {
            _countries = countries;
        }

        // GET api/country
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _countries.GetAll());
        }
    }
}
