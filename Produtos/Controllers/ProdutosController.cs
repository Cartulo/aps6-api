using Aps6Api.Produtos;
using Aps6Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("_Aps6policy")]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutosService _produtosService;
    public ProdutosController(ProdutosService produtosService) => _produtosService = produtosService;

    [HttpGet]
    public async Task<List<Produto>> Get() => await _produtosService.GetTodosProdutos();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Produto>> Get(string id)
    {
        var produto = await _produtosService.GetPorId(id);

        if (produto is null) return NotFound();

        return produto;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Produto novoProduto)
    {
        await _produtosService.AdicionarProduto(novoProduto);

        return CreatedAtAction(nameof(Get), new { id = novoProduto.Id }, novoProduto);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Produto produtoAtualizado)
    {
        var produto = await _produtosService.GetPorId(id);

        if (produto is null) return NotFound();

        produtoAtualizado.Id = produto.Id;

        await _produtosService.AtualizarProduto(id, produtoAtualizado);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var produto = await _produtosService.GetPorId(id);

        if (produto is null) return NotFound();

        await _produtosService.ExcluirProduto(id);

        return NoContent();
    }
}