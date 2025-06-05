using BizBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace BizBoard.Infrastructure.Persistence
{
    public class BizBoardDbContext : DbContext
    {
        public BizBoardDbContext(DbContextOptions<BizBoardDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
            });
        }
    }
}
