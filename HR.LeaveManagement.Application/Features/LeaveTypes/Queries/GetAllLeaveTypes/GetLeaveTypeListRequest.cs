using HR.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypeListRequest : IRequest<List<LeaveTypeDto>>;
}
