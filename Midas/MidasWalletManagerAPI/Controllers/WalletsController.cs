using Application.Interfaces;
using Application.Wallets.Queries.GetWalletsList;
using Data.Wallets;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;
        private readonly IGetWalletsListQuery _getWalletsListQuery;

        public WalletsController(IDatabaseService databaseService, IGetWalletsListQuery getWalletsListQuery)
        {
            _databaseService = databaseService;
            _getWalletsListQuery = getWalletsListQuery;
        }

        // GET: api/wallets
        [HttpGet]
        public ActionResult<IEnumerable<WalletWithBalancesDto>> Get()
        {
            var dtos = _getWalletsListQuery.Execute();

            return Ok(dtos);
        }
    }
}
