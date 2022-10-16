using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Aps6Api.Models
{
    public class Aps6Context : DbContext
    {
        public Aps6Context(DbContextOptions<Aps6Context> options)
            : base(options)
        {
        }

        public DbSet<Aps6Item> Aps6Items { get; set; } = null!;
    }
}