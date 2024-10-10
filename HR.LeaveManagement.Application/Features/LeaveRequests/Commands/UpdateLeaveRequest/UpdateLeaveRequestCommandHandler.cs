using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Infrastructure;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Models.Email;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler(IMapper mapper, IEmailSender emailSender,
        IAppLogger<UpdateLeaveAllocationCommandHandler> appLogger,
        ILeaveRequestRepository leaveRequestRepository, ILeaveTypeRepository leaveTypeRepository) 
        : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository = leaveRequestRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository = leaveTypeRepository;

        private readonly IMapper _mapper = mapper;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _appLogger = appLogger;

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

            if (leaveRequest is null) {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            var validator = new UpdateLeaveRequestDtoValidator(_leaveTypeRepository, _leaveRequestRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto, cancellationToken);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            _mapper.Map(request.LeaveRequestDto, leaveRequest);
            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            SendLeaveRequestEmail(request.LeaveRequestDto.StartDate, request.LeaveRequestDto.EndDate);

            return Unit.Value;
        }

        private async void SendLeaveRequestEmail(DateTime startDate, DateTime endDate)
        {
            try
            {
                var email = new Email
                {
                    To = string.Empty, // Get email from Employedd record
                    Body = $"Your leave request for {startDate:D} to {endDate:D} has been update successfully.",
                    Subject = "Leave Request Updated"
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
