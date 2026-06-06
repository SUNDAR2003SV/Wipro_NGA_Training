using HRManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace HRManagement.Infrastructure.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(
            DbContextOptions<HRDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Leave> Leaves { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Leave>()
                .HasOne(l => l.Employee)
                .WithMany(e => e.Leaves)
                .HasForeignKey(l => l.EmployeeId);
        }
    }
}