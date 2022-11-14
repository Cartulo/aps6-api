using Microsoft.EntityFrameworkCore;

namespace Aps6Api.Setores.Contexts;

public class SetoresContext : DbContext
{
    public SetoresContext(DbContextOptions<SetoresContext> options) : base(options) { }
    public DbSet<Setor> Setores { get; set; } = null!;
}