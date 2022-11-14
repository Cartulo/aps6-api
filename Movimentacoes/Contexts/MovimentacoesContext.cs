using Microsoft.EntityFrameworkCore;

namespace Aps6Api.Movimentacoes.Contexts;

public class MovimentacoesContext : DbContext
{
    public MovimentacoesContext(DbContextOptions<MovimentacoesContext> options) : base(options) { }
    public DbSet<Movimentacao> Movimentacoes { get; set; } = null!;
}