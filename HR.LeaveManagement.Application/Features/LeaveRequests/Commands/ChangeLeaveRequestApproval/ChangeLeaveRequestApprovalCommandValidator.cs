using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveRequests.Commands.ChangeLeaveRequestApproval
{
    public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
    {
        public ChangeLeaveRequestApprovalCommandValidator()
        {
            RuleFor(p => p.IsApproved)
                .NotNull()
                .WithMessage("Approval status cannot be null");
        }
    }
}
