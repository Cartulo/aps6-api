using Aps6Api.Setores;
using Aps6Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("MyPolicy")]
[Route("api/setores")]
public class SetoresController : ControllerBase
{
    private readonly SetoresService _setoresService;

    public SetoresController(SetoresService setoresService) =>
        _setoresService = setoresService;

    [HttpGet]
    public async Task<List<Setor>> Get() => await _setoresService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Setor>> Get(string id)
    {
        var Setor = await _setoresService.GetAsync(id);

        if (Setor is null)
        {
            return NotFound();
        }

        return Setor;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Setor newSetor)
    {
        await _setoresService.CreateAsync(newSetor);

        return CreatedAtAction(nameof(Get), new { id = newSetor.Id }, newSetor);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Setor updatedSetor)
    {
        var Setor = await _setoresService.GetAsync(id);

        if (Setor is null)
        {
            return NotFound();
        }

        updatedSetor.Id = Setor.Id;

        await _setoresService.UpdateAsync(id, updatedSetor);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Setor = await _setoresService.GetAsync(id);

        if (Setor is null)
        {
            return NotFound();
        }

        await _setoresService.RemoveAsync(id);

        return NoContent();
    }
}