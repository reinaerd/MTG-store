using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_lib.Services;
using mtg_app.Data;
using mtg_lib.Library.Models;


namespace Microsoft.EntityFrameworkCore
{
    [Route("api/controller")]
    [ApiController]

    public class CardController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        
        public CardController(ApplicationDbContext context)
        {
            _context = context;
        }




        [HttpGet("{page}")]
        
        public async Task<ActionResult<List<Card>>> GetCards(int page)
        {

            if(_context.Cards == null)
                return NotFound();

            float pageResults = 3f;
            double pageCount = Math.Ceiling(_context.Cards.Count() / pageResults);

            List<Card> cards = await _context.Cards
            .Skip((page - 1) * (int)pageResults)
            .Take((int)pageResults)
            .ToListAsync();

            

            return Ok(new CardResponseDTO {
                Cards = cards,
                CurrentPage = page,
                Pages = (int)pageCount
            });

        }


    }
}