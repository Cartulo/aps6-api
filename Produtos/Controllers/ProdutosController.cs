using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aps6Api.Produtos;
using Aps6Api.Produtos.Contexts;

namespace Aps6Api.Produtos_Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosContext _context;

        public ProdutosController(ProdutosContext context)
        {
            _context = context;
        }

        // GET: api/Produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            return await _context.Produtos
                .Select(x => ProdutoToDTO(x))
                .ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(long id)
        {
            var Produto = await _context.Produtos.FindAsync(id);

            if (Produto == null)
                return NotFound();

            return ProdutoToDTO(Produto);
        }

        // PUT: api/Produtos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduto(/*long*/Guid id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.Id)
                return BadRequest();

            var Produto = await _context.Produtos.FindAsync(id);
            if (Produto == null)
                return NotFound();

            Produto.Nome = produtoDTO.Nome;
            Produto.Setor = produtoDTO.Setor;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ProdutoExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Produtos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> CreateProduto(ProdutoDTO produtoDTO)
        {
            var Produto = new Produto
            {
                Nome = produtoDTO.Nome,
                Setor = produtoDTO.Setor,
            };

            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProduto),
                new { id = Produto.Id },
                ProdutoToDTO(Produto));
        }

        // DELETE: api/Produtos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduto(/*long*/Guid id)
        {
            var Produto = await _context.Produtos.FindAsync(id);

            if (Produto == null)
                return NotFound();

            _context.Produtos.Remove(Produto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProdutoExists(Guid id)
        {
            return (_context.Produtos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static ProdutoDTO ProdutoToDTO(Produto produto) =>
            new ProdutoDTO
            {
                Nome = produto.Nome,
                Setor = produto.Setor,
            };
    }
}
