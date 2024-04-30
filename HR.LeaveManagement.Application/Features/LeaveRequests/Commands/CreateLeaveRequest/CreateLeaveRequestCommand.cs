using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Responses;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : IRequest<Unit>
    {
        public required CreateLeaveRequestDto LeaveRequestDto { get; set; }
    }
}
