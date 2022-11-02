using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aps6Api.Setores;
using Aps6Api.Setores.Contexts;

namespace Aps6Api.Setores_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetoresController : ControllerBase
    {
        private readonly SetoresContext _context;

        public SetoresController(SetoresContext context)
        {
            _context = context;
        }

        // GET: api/Setores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setor>>> GetSetores()
        {
          if (_context.Setores == null)
          {
              return NotFound();
          }
            return await _context.Setores.ToListAsync();
        }

        // GET: api/Setores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Setor>> GetSetor(Guid id)
        {
          if (_context.Setores == null)
          {
              return NotFound();
          }
            var setor = await _context.Setores.FindAsync(id);

            if (setor == null)
            {
                return NotFound();
            }

            return setor;
        }

        // PUT: api/Setores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetor(Guid id, Setor setor)
        {
            if (id != setor.Id)
            {
                return BadRequest();
            }

            _context.Entry(setor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Setores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Setor>> PostSetor(Setor setor)
        {
          if (_context.Setores == null)
          {
              return Problem("Entity set 'SetoresContext.Setores'  is null.");
          }
            _context.Setores.Add(setor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSetor", new { id = setor.Id }, setor);
        }

        // DELETE: api/Setores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(Guid id)
        {
            if (_context.Setores == null)
            {
                return NotFound();
            }
            var setor = await _context.Setores.FindAsync(id);
            if (setor == null)
            {
                return NotFound();
            }

            _context.Setores.Remove(setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetorExists(Guid id)
        {
            return (_context.Setores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
