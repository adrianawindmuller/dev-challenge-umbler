using Desafio.Umbler.Domain;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Umbler.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<DomainHost> DomainHost { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new DomainHostMap());
        }
    }
}