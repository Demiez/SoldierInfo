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


        [HttpGet("all")]
        public async Task<IActionResult> GetAllSoldiers()
        {
            List<Soldier> soldiers = _context.getSoldiers();
            return Ok(soldiers);
        }

        [HttpGet("add")]
        public async Task<IActionResult> AddSoldier()
        {
            var soldier = new Soldier { Name = "Jane" };
            await _context.Soldiers.AddAsync(soldier);
            var answer = await _context.SaveChangesAsync();
            _logger.LogInformation($"Added soldier {soldier.Name} with id:{answer}");
            return Ok(answer);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
