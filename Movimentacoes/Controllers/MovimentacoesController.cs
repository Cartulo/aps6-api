using Aps6Api.Movimentacoes;
using Aps6Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("MyPolicy")]
[Route("api/movimentacoes")]
public class MovimentacoesController : ControllerBase
{
    private readonly MovimentacoesService _movimentacoesService;

    public MovimentacoesController(MovimentacoesService movimentacoesService) =>
        _movimentacoesService = movimentacoesService;

    [HttpGet]
    public async Task<List<Movimentacao>> Get() => await _movimentacoesService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Movimentacao>> Get(string id)
    {
        var Movimentacao = await _movimentacoesService.GetAsync(id);

        if (Movimentacao is null)
        {
            return NotFound();
        }

        return Movimentacao;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Movimentacao newMovimentacao)
    {
        await _movimentacoesService.CreateAsync(newMovimentacao);

        return CreatedAtAction(nameof(Get), new { id = newMovimentacao.Id }, newMovimentacao);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Movimentacao updatedMovimentacao)
    {
        var Movimentacao = await _movimentacoesService.GetAsync(id);

        if (Movimentacao is null)
        {
            return NotFound();
        }

        updatedMovimentacao.Id = Movimentacao.Id;

        await _movimentacoesService.UpdateAsync(id, updatedMovimentacao);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Movimentacao = await _movimentacoesService.GetAsync(id);

        if (Movimentacao is null)
        {
            return NotFound();
        }

        await _movimentacoesService.RemoveAsync(id);

        return NoContent();
    }
}