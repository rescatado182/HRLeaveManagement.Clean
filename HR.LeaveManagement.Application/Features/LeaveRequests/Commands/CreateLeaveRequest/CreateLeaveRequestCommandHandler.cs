using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveRequestCommand> _appLogger;
        public CreateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender,
            IAppLogger<CreateLeaveRequestCommand> applogger,
            ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository
        )
        {
            _leaveRequestRepository = leaveRequestRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _emailSender = emailSender;
            _mapper = mapper;
            _appLogger = applogger;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestDtoValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);

            if (validationResult.Errors.Count != 0) {
                throw new BadRequestException("Invalid Leave Request", validationResult);
            }

            // Create Leave Request
            var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
            leaveRequest = await _leaveRequestRepository.CreateAsync(leaveRequest);

            SendLeaveRequestEmail(leaveRequest);

            return Unit.Value;
        }

        private async void SendLeaveRequestEmail(LeaveRequest leaveRequest)
        {
            try
            {
                var email = new Email
                {
                    To = string.Empty,
                    Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} " +
                    $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"

                };

                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarning(ex.Message);
                throw;
            }
            return;
        }
    }
}
