using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aps6Api.Models;

namespace Aps6Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class Aps6ItemsController : ControllerBase
    {
        private readonly Aps6Context _context;

        public Aps6ItemsController(Aps6Context context)
        {
            _context = context;
        }

        // GET: api/Aps6Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aps6ItemDTO>>> GetAps6Items()
        {
            if (_context.Aps6Items == null)
            {
                return NotFound();
            }
            return await _context.Aps6Items
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // GET: api/Aps6Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aps6ItemDTO>> GetAps6Item(long id)
        {
            if (_context.Aps6Items == null)
            {
                return NotFound();
            }
            var aps6Item = await _context.Aps6Items.FindAsync(id);

            if (aps6Item == null)
            {
                return NotFound();
            }

            return ItemToDTO(aps6Item);
        }

        // PUT: api/Aps6Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, Aps6ItemDTO aps6ItemDTO)
        {
            if (id != aps6ItemDTO.Id)
            {
                return BadRequest();
            }

            var aps6Item = await _context.Aps6Items.FindAsync(id);
            if (aps6Item == null)
            {
                return NotFound();
            }

            aps6Item.Name = aps6ItemDTO.Name;
            aps6Item.IsComplete = aps6ItemDTO.IsComplete;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!Aps6ItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Aps6Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aps6ItemDTO>> CreateAps6Item(Aps6ItemDTO aps6ItemDTO)
        {
            var aps6Item = new Aps6Item
            {
                IsComplete = aps6ItemDTO.IsComplete,
                Name = aps6ItemDTO.Name
            };

            _context.Aps6Items.Add(aps6Item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAps6Item),
                new { id = aps6Item.Id },
                ItemToDTO(aps6Item));
        }

        // DELETE: api/Aps6Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAps6Item(long id)
        {
            var aps6Item = await _context.Aps6Items.FindAsync(id);

            if (aps6Item == null)
            {
                return NotFound();
            }

            _context.Aps6Items.Remove(aps6Item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Aps6ItemExists(long id)
        {
            return (_context.Aps6Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static Aps6ItemDTO ItemToDTO(Aps6Item aps6Item) =>
            new Aps6ItemDTO
            {
                Id = aps6Item.Id,
                Name = aps6Item.Name,
                IsComplete = aps6Item.IsComplete
            };
    }
}
