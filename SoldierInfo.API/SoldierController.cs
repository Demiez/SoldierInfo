using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoldierInfo.Domain;
using SoldierInfo.Data;
using Microsoft.EntityFrameworkCore;

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
            var soldiers = await _context.Soldiers
                .OrderBy(s => s.Id)
                .ToArrayAsync();
            return Ok(soldiers);
        }

        //[HttpGet("add")]
        //public async Task<IActionResult> AddSoldier()
        //{
        //    var soldier = new Soldier { Name = "Jane" };
        //    await _context.Soldiers.AddAsync(soldier);
        //    var answer = await _context.SaveChangesAsync();
        //    _logger.LogInformation($"Added soldier {soldier.Name} with id:{answer}");
        //    return Ok(answer);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSoldier(int id)
        {
            var soldier = await _context.Soldiers.FindAsync(id);
            if (soldier == null)
            {
                return NotFound();
            }

            return Ok(soldier);
        }

        [HttpPost("new")]
        public async Task<IActionResult> AddSoldier(Soldier model)
        {
            List<Quote> SoldierQuotes = new List<Quote>();
            List<Battle> Battles = new List<Battle>();

            if (!model.Quotes.Any())
            {
                foreach(var quote in model.Quotes)
                {
                    SoldierQuotes.Add(new Quote { Text = quote.Text });
                }

                if (model.Quotes == null)
                    return NotFound();
            }

            if (!model.SoldierBattles.Any())
            {
                foreach (var battle in model.SoldierBattles)
                {
                    Battles.Add(new Battle { 
                        Name = battle.Battle.Name
                    });
                }

                if (model.SoldierBattles == null)
                    return NotFound();
            }

            var soldier = new Soldier
            {
                Name = model.Name,
                Quotes = SoldierQuotes
            };
                
                
             _context.Soldiers.Add(soldier);

            await _context.SaveChangesAsync();

            return Ok(soldier);
        }

        [HttpPatch("name/{id}")]
        public async Task<IActionResult> UpdateSoldierName(
            [FromRoute] int id,
            [FromQuery] string newName
            )
        {
            var soldier = await _context.Soldiers.FindAsync(id);

            if (soldier == null)
                return NotFound();

            soldier.Name = newName;

            await _context.SaveChangesAsync();

            return Ok(soldier);
        }

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoldier(int id)
        {
            var soldier = await _context.Soldiers.FindAsync(id);
            if (soldier == null)
            {
                return NotFound();
            }
            _context.Soldiers.Remove(soldier);
            await _context.SaveChangesAsync();
            return Ok(soldier.Id);
        }
    }
}
