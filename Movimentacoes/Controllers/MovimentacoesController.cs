using Aps6Api.Movimentacoes;
using Aps6Api.Movimentacoes.Queries;
using Aps6Api.Movimentacoes.Requests;
using Aps6Api.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Aps6Api.Controllers;

[ApiController]
[EnableCors("_Aps6policy")]
[Route("api/movimentacoes")]
public class MovimentacoesController : ControllerBase
{
    private readonly MovimentacoesService _movimentacoesService;
    private readonly ProdutosService _produtosService;

    public MovimentacoesController(
        MovimentacoesService movimentacoesService,
        ProdutosService produtosService) 
    {
        _movimentacoesService = movimentacoesService;
        _produtosService = produtosService;
    }

    [HttpGet]
    public async Task<List<Movimentacao>> Get() => await _movimentacoesService.GetTodasMovimentacoes();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Movimentacao>> Get(string id)
    {
        var Movimentacao = await _movimentacoesService.GetMovimentacaoPorId(id);

        if (Movimentacao is null) return NotFound();

        return Movimentacao;
    }

    [HttpPost("por-produto")]
    public async Task<MovimentacaoProdutoQuantidadesQuery?> GetPorProduto(string produtoId)
    {
        var produto = await _produtosService.GetPorId(produtoId);
        var setoresId = produto?.SetoresId;

        var movimentacao = new List<Movimentacao>();

        var quantidadeEntrada = 0;
        var quantidadeSaida = 0;

        foreach (var setorId in setoresId)
        {
            var movimentacaoAtual = new List<Movimentacao>();
            var movimentacaoFutura = new List<Movimentacao>();
            
            movimentacaoAtual.AddRange(await _movimentacoesService.GetMovimentacoesPorSetorAtualId(setorId, produtoId));
            movimentacaoFutura.AddRange(await _movimentacoesService.GetMovimentacoesPorSetorFuturoId(setorId, produtoId));
        
            quantidadeEntrada = quantidadeEntrada + movimentacaoAtual.Sum(o => o.SetorAtualId == setorId && o.SetorFuturoId != "637387a9cb27f8f2f536e482" ? o.Quantidade : 0);
            // Id chumbado do "setor" saida
            quantidadeSaida = quantidadeSaida + movimentacaoFutura.Sum(o => o.SetorFuturoId == "637387a9cb27f8f2f536e482" ? o.Quantidade : 0);

            movimentacao.AddRange(movimentacaoAtual);
            // TODO : Provavelmente sera necessario colocar uma validacao os ID chumbados tipo EntradaDeFora e SaidaPraFora
        }

        var quantidadeTotal = quantidadeEntrada - quantidadeSaida;

        var movimentacaoQuantidades = new MovimentacaoProdutoQuantidadesQuery(movimentacao, produtoId, quantidadeEntrada, quantidadeSaida, quantidadeTotal);

        if (movimentacaoQuantidades is null) return null;

        return movimentacaoQuantidades;
    }

    [HttpPost("por-setor")]
    public async Task<MovimentacaoProdutoQuantidadesPorSetorQuery?> GetPorSetor(ObterQuantidadesPorSetorRequest request)
    {
        var movimentacaoAtual = await _movimentacoesService.GetMovimentacoesPorSetorAtualId(request.SetorId, request.ProdutoId);
        var movimentacaoFutura = await _movimentacoesService.GetMovimentacoesPorSetorFuturoId(request.SetorId, request.ProdutoId);

        var movimentacao = new List<Movimentacao>();
        movimentacao.AddRange(movimentacaoAtual);
        movimentacao.AddRange(movimentacaoFutura);

        var quantidadeEntrada = movimentacaoAtual.Sum(o => o.SetorAtualId == request.SetorId ? o.Quantidade : 0);
        var quantidadeSaida = movimentacaoFutura.Sum(o => o.SetorFuturoId == request.SetorId ? o.Quantidade : 0);
        var quantidadeTotal = quantidadeEntrada - quantidadeSaida;

        var movimentacaoQuantidades = new MovimentacaoProdutoQuantidadesPorSetorQuery(movimentacao, request.SetorId, quantidadeEntrada, quantidadeSaida, quantidadeTotal);

        if (movimentacaoQuantidades is null) return null;

        return movimentacaoQuantidades;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Movimentacao novaMovimentacao)
    {
        await _movimentacoesService.AdicionarMovimentacao(novaMovimentacao);

        return CreatedAtAction(nameof(Get), new { id = novaMovimentacao.Id }, novaMovimentacao);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Movimentacao movimentacaoAtualizada)
    {
        var Movimentacao = await _movimentacoesService.GetMovimentacaoPorId(id);

        if (Movimentacao is null) return NotFound();

        movimentacaoAtualizada.Id = Movimentacao.Id;

        await _movimentacoesService.AtualizarMovimentacao(id, movimentacaoAtualizada);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var Movimentacao = await _movimentacoesService.GetMovimentacaoPorId(id);

        if (Movimentacao is null) return NotFound();

        await _movimentacoesService.ExcluirMovimentacao(id);

        return NoContent();
    }
}