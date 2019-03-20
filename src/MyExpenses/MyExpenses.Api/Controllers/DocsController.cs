using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyExpenses.Api.Controllers
{
    [Route("/")]
    public class DocsController : ControllerBase
    {
        [HttpGet("/")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Swagger()
        {
            return Redirect("/swagger");
        }
    }
}
