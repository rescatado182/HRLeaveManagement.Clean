namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class CreateLeaveTypeDto : ILeaveTypeDto
    {
        public required string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
