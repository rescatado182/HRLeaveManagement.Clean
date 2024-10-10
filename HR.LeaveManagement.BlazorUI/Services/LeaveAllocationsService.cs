using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class LeaveAllocationsService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationsService(IClient client) : base(client)
        {
        }
    }
}
