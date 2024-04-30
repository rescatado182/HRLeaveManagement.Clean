using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public UpdateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveRequestRepository = leaveRequestRepository;

            Include(new ILeaveRequestDtoValidator(_leaveTypeRepository));

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExists)
                .WithMessage("{PropertyName} must be present.");
        }

        private async Task<bool> LeaveRequestMustExists(int id, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveRequestRepository.GetByIdAsync(id);
            return leaveAllocation != null;
        }
    }
}
