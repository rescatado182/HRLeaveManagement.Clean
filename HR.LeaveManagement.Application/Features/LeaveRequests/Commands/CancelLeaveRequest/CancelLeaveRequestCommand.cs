using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
