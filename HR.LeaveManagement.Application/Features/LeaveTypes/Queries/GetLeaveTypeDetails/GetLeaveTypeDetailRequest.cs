using HR.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetLeaveTypeDetails
{
    public record GetLeaveTypeDetailRequest(int Id) : IRequest<LeaveTypeDto>;

}
