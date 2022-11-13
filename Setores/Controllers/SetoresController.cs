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
    [Route("api/setores")]
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
        public async Task<ActionResult<IEnumerable<SetorDTO>>> GetSetores()
        {
            return await _context.Setores
                .Select(x => SetorToDTO(x))
                .ToListAsync();
        }

        // GET: api/Setores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SetorDTO>> GetSetor(long id)
        {
            var Setor = await _context.Setores.FindAsync(id);

            if (Setor == null)
                return NotFound();

            return SetorToDTO(Setor);
        }

        // PUT: api/Setores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSetor(/*long*/Guid id, SetorDTO setorDTO)
        {
            if (id != setorDTO.Id)
                return BadRequest();

            var Setor = await _context.Setores.FindAsync(id);
            if (Setor == null)
                return NotFound();

            Setor.Nome = setorDTO.Nome;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!SetorExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Setores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SetorDTO>> CreateSetor(SetorDTO setorDTO)
        {
            var Setor = new Setor
            {
                Nome = setorDTO.Nome
            };

            _context.Setores.Add(Setor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetSetor),
                new { id = Setor.Id },
                SetorToDTO(Setor));
        }

        // DELETE: api/Setores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSetor(/*long*/Guid id)
        {
            var Setor = await _context.Setores.FindAsync(id);

            if (Setor == null)
                return NotFound();

            _context.Setores.Remove(Setor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SetorExists(Guid id)
        {
            return (_context.Setores?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static SetorDTO SetorToDTO(Setor setor) =>
            new SetorDTO
            {
                Nome = setor.Nome
            };
    }
}
