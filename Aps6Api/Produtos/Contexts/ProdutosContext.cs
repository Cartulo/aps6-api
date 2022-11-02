using Microsoft.EntityFrameworkCore;

namespace Aps6Api.Produtos.Contexts;

public class ProdutosContext : DbContext
{
    public ProdutosContext(DbContextOptions<ProdutosContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos {get ; set; } = null!;
}