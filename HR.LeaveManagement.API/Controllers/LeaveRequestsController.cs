using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequests.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeaveRequestsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<LeaveRequestsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
        {
            var leaveRequests = await _mediator.Send( new GetLeaveRequestListRequest() );
            return Ok( leaveRequests );
        }

        // GET api/<LeaveRequestsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveRequestDto>> Get(int id)
        {
            var leaveRequest = await _mediator.Send( new GetLeaveAllocationDetailRequest { Id = id } );
            return Ok( leaveRequest );
        }

        // POST api/<LeaveRequestsController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Post(CreateLeaveRequestCommand leaveRequestCommand)
        {
            var response = await _mediator.Send( leaveRequestCommand );
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestsController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Put(UpdateLeaveRequestCommand leaveRequestCommand)
        {
            await _mediator.Send( leaveRequestCommand );
            return NoContent();
        }

        // DELETE api/<LeaveRequestsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT api/<LeaveRequestsController>/CancelRequest
        [HttpPut]
        [Route("CancelRequest")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> CancelRequest(CancelLeaveRequestCommand cancelLeaveRequest)
        {
            await _mediator.Send(cancelLeaveRequest);
            return NoContent();
        }

        // PUT api/<LeaveRequestsController>/UpdateApproval
        [HttpPut]
        [Route("UpdateApproval")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateApproval(ChangeLeaveRequestApprovalCommand leaveRequestApprovalCommand)
        {
            await _mediator.Send(leaveRequestApprovalCommand);
            return NoContent();
        }
    }
}
