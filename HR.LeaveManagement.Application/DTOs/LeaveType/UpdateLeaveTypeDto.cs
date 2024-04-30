using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.DTOs.LeaveType
{
    public class UpdateLeaveTypeDto : ILeaveTypeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int DefaultDays { get; set; }
    }
}
