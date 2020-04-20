using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoldierInfo.Domain;
using SoldierInfo.Data;

namespace SoldierInfo.API
{
    [ApiController]
    [Route("[controller]")]
    public class SoldierController : ControllerBase
    {
        private readonly ILogger<SoldierController> _logger;
        private readonly SoldierContext _context;

        public SoldierController(ILogger<SoldierController> logger, SoldierContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllSoldiers()
        {
            List<Soldier> soldiers = _context.getSoldiers();
            return Ok(soldiers);
        }
    }
}
