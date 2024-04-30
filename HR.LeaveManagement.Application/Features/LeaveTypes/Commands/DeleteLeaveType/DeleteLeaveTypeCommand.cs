using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
