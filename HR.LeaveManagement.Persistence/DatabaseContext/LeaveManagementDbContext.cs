﻿using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext
{
    public class LeaveManagementDbContext : DbContext
    {
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }        
        
        public LeaveManagementDbContext(DbContextOptions<LeaveManagementDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LeaveManagementDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
            {
                entry.Entity.DateModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }


            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
