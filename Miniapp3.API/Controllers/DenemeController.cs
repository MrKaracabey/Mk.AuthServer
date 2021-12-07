using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Miniapp3.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
           

            return Ok("BEn bir denemeyim");
        }
    }
}