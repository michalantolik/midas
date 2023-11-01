using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public WalletsController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
         
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
