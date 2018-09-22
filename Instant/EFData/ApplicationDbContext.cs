using Instant.EFData;
using Microsoft.EntityFrameworkCore;

namespace Instant.EFData
{
    public class ApplicationDbContext: DbContext
    {

        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<CardAccount> CardAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CardAccount>();

            base.OnModelCreating(builder);
        }
    }
}
