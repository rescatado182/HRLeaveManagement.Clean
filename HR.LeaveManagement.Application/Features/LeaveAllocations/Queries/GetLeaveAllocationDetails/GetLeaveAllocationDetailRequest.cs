using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailRequest : IRequest<LeaveAllocationDetailsDto>
    {
        public int Id { get; set; }
    }
}
