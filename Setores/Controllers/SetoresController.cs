using Aps6Api.Setores;
using Aps6Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("_Aps6policy")]
[Route("api/setores")]
public class SetoresController : ControllerBase
{
    private readonly SetoresService _setoresService;

    public SetoresController(SetoresService setoresService) => _setoresService = setoresService;

    [HttpGet]
    public async Task<List<Setor>> Get() => await _setoresService.GetTodosSetores();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Setor>> Get(string id)
    {
        var Setor = await _setoresService.GetPorId(id);

        if (Setor is null) return NotFound();

        return Setor;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Setor novoSetor)
    {
        await _setoresService.AdicionarSetor(novoSetor);

        return CreatedAtAction(nameof(Get), new { id = novoSetor.Id }, novoSetor);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Setor setorAtualizado)
    {
        var Setor = await _setoresService.GetPorId(id);

        if (Setor is null) return NotFound();

        setorAtualizado.Id = Setor.Id;

        await _setoresService.AtualizarSetor(id, setorAtualizado);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Setor = await _setoresService.GetPorId(id);

        if (Setor is null) return NotFound();

        await _setoresService.ExcluirSetor(id);

        return NoContent();
    }
}