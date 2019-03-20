using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyExpenses.Api.Infrastructure.HttpErros;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenses.Api.Controllers
{
    [Route("api/heroes")]
    [Authorize]
    public class HeroesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(HttpError), StatusCodes.Status500InternalServerError)]
        public IActionResult Get()
        {
            return Ok(new List<Hero>
            {
                new Hero{Id=1, Name="Jose"},
                new Hero{Id=2, Name="SuperMan"},
            });
        }
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
