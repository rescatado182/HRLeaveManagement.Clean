using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.DeleteLeaveRequest
{
    public class DeleteLeaveRequestCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
