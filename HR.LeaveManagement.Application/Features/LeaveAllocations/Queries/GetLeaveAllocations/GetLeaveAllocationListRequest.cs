using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations
{
    public class GetLeaveAllocationListRequest : IRequest<List<LeaveAllocationDto>>
    {

    }
}
