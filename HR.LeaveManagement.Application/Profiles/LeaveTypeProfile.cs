using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Profiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<CreateLeaveTypeDto, LeaveType>();
            CreateMap<UpdateLeaveTypeDto, LeaveType>();
        }
    }
}
