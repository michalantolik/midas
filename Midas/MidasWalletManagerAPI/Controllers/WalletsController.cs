using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MidasWalletManagerAPI.Controllers
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
    }
}
