using Microsoft.EntityFrameworkCore;
using OnOut.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnOut.Persistance.DatabaseContext
{
    public class OnOutDbContext:DbContext
    {
        public OnOutDbContext(DbContextOptions<OnOutDbContext> options):base(options)
        {
            
        }

        public DbSet<Hasher> Hashers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OnOutDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.ModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
