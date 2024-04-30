using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmailSender _emailSender;
        public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository, IEmailSender emailSender)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Cancelled = true;

            // if requqest is approved, re-evaluate the employee's allocations for leave type

            // Send confirmation email
            SendLeaveRequestCancellationEmail(leaveRequest.StartDate, leaveRequest.EndDate);

            return Unit.Value;
        }

        private async void SendLeaveRequestCancellationEmail(DateTime startDate, DateTime endDate)
        {
            var email = new Email
            {
                To = string.Empty,
                Body = $"Your leave request for {startDate:D} to {endDate:D} " +
                $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            await _emailSender.SendEmail(email);

            return;
        }
    }
}
