using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Miniapp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
            //Gelen Tokendaki bilgiler Client için oradan çekilebilir haldedir bilgine :)
            var username = HttpContext.User.Identity.Name;

            return Ok($"Your Username is {username}");
        }
    }
}