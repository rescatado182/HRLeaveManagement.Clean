using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        private readonly HrDatabaseContext _dbContext;
        public LeaveAllocationRepository(HrDatabaseContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _dbContext.AddRangeAsync(allocations);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int levaTypeId, int period)
        {
            return await _dbContext.LeaveAllocations.AnyAsync
                (
                    q => q.EmployeeId == userId
                    && q.LeaveTypeId == levaTypeId
                    && q.Period == period
                );
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocation = await _dbContext.LeaveAllocations
                .Include (q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            return leaveAllocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _dbContext.LeaveAllocations
                .Where(q => q.EmployeeId.Equals(userId))
                .Include(q => q.LeaveType)
                .ToListAsync();

            return leaveAllocations;

        }

        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _dbContext.LeaveAllocations
                        .FirstOrDefaultAsync(q => q.EmployeeId == userId
                                            && q.LeaveTypeId == leaveTypeId);
        }
    }
}
