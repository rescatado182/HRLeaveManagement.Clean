using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandHandler : IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public ChangeLeaveRequestApprovalCommandHandler(IMapper mapper, IEmailSender emailSender,
            ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;

            _mapper = mapper;
            _emailSender = emailSender;
        }

        public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Approved = request.IsApproved;
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            // if request is approved, get and update the employee's allocations
            SendLeaveRequestChangeApprovalEmail(leaveRequest.StartDate, leaveRequest.EndDate);

            return Unit.Value;
        }

        private async void SendLeaveRequestChangeApprovalEmail(DateTime startDate, DateTime endDate)
        {
            var email = new Email
            {
                To = string.Empty, // Get email from employee record
                Body = $"The approval status for your leave request for {startDate:D} to {endDate:D} " +
                $"has been updated.",
                Subject = "Leave Request Approval Status Updated"
            };

            await _emailSender.SendEmail(email);

            return;
        }
    }
}
