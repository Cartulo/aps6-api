using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aps6Api.Movimentacoes;
using Aps6Api.Movimentacoes.Contexts;

namespace Aps6Api.Movimentacoes_Controllers
{
    [Route("api/movimentacoes")]
    [ApiController]
    public class MovimentacoesController : ControllerBase
    {
        private readonly MovimentacoesContext _context;

        public MovimentacoesController(MovimentacoesContext context)
        {
            _context = context;
        }

        // GET: api/Movimentacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimentacaoDTO>>> GetMovimentacoes()
        {
            return await _context.Movimentacoes
                .Select(x => MovimentacaoToDTO(x))
                .ToListAsync();
        }

        // GET: api/Movimentacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimentacaoDTO>> GetMovimentacao(long id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);

            if (movimentacao == null)
                return NotFound();

            return MovimentacaoToDTO(movimentacao);
        }

        // PUT: api/Movimentacoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovimentacao(/*long*/Guid id, MovimentacaoDTO movimentacaoDTO)
        {
            if (id != movimentacaoDTO.Id)
                return BadRequest();

            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
                return NotFound();

            movimentacao.Nome = movimentacaoDTO.Nome;
            movimentacao.Quantidade = movimentacaoDTO.Quantidade;
            movimentacao.QuantidadeMinima = movimentacaoDTO.QuantidadeMinima;
            movimentacao.DataEntrada = movimentacaoDTO.DataEntrada;
            movimentacao.DataSaida = movimentacaoDTO.DataSaida;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MovimentacaoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Movimentacoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MovimentacaoDTO>> CreateMovimentacao(MovimentacaoDTO movimentacaoDTO)
        {
            var movimentacao = new Movimentacao
            {
                Nome = movimentacaoDTO.Nome,
                Quantidade = movimentacaoDTO.Quantidade,
                QuantidadeMinima = movimentacaoDTO.QuantidadeMinima,
                DataEntrada = movimentacaoDTO.DataEntrada,
                DataSaida = movimentacaoDTO.DataSaida
            };

            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMovimentacao),
                new { id = movimentacao.Id },
                MovimentacaoToDTO(movimentacao));
        }

        // DELETE: api/Movimentacoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimentacao(/*long*/Guid id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);

            if (movimentacao == null)
                return NotFound();

            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimentacaoExists(Guid id)
        {
            return (_context.Movimentacoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static MovimentacaoDTO MovimentacaoToDTO(Movimentacao movimentacao) =>
            new MovimentacaoDTO
            {
                Nome = movimentacao.Nome,
                Quantidade = movimentacao.Quantidade,
                QuantidadeMinima = movimentacao.QuantidadeMinima,
                DataEntrada = movimentacao.DataEntrada,
                DataSaida = movimentacao.DataSaida
            };
    }
}
