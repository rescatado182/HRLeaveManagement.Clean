namespace HR.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDto : ILeaveAllocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
