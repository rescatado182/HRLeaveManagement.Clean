using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveRequest : BaseDomainEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required LeaveType LeaveType { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime DateRequested { get; set; }
        public required string RequestComments { get; set; }
        public DateTime? DateActioned { get; set; }
        public bool? Approved { get; set; }
        public bool Cancelled { get; set; }

        public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}
