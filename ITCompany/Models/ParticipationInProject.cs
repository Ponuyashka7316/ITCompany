using System;
using System.Collections.Generic;

namespace ITCompany.Models
{
    public partial class ParticipationInProject
    {
        public long ProjectId { get; set; }
        public long EmployeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StopDate { get; set; }

        public EmployeeInfo Employee { get; set; }
        public ProjectInfo Project { get; set; }
    }
}
