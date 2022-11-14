using Aps6Api.Produtos;
using Aps6Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("MyPolicy")]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly ProdutosService _produtosService;
    public ProdutosController(ProdutosService produtosService) => _produtosService = produtosService;

    [HttpGet]
    public async Task<List<Produto>> Get() => await _produtosService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Produto>> Get(string id)
    {
        var produto = await _produtosService.GetAsync(id);

        if (produto is null) return NotFound();

        return produto;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Produto newProduto)
    {
        await _produtosService.CreateAsync(newProduto);

        return CreatedAtAction(nameof(Get), new { id = newProduto.Id }, newProduto);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Produto updatedProduto)
    {
        var produto = await _produtosService.GetAsync(id);

        if (produto is null) return NotFound();

        updatedProduto.Id = produto.Id;

        await _produtosService.UpdateAsync(id, updatedProduto);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var produto = await _produtosService.GetAsync(id);

        if (produto is null) return NotFound();

        await _produtosService.RemoveAsync(id);

        return NoContent();
    }
}