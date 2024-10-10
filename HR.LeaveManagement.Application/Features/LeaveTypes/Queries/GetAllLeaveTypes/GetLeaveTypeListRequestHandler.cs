using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly IAppLogger<GetLeaveTypeListRequestHandler> _logger;

        public GetLeaveTypeListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper,
            IAppLogger<GetLeaveTypeListRequestHandler> logger)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            // Query the Database
            var leaveTypes = await _leaveTypeRepository.GetAsync();

            // convert data objects to DTO objects
            var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
                        
            _logger.LogInformation("Leave Types were retrieved successfully");
            return data;
        }

    }
}
