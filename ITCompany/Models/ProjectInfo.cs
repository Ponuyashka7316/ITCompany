using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITCompany.Models
{
    public partial class ProjectInfo
    {
        public ProjectInfo()
        {
            EmployeeInfo = new HashSet<EmployeeInfo>();
            ParticipationInProject = new HashSet<ParticipationInProject>();
        }
        [Key]
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime? ProjectStart { get; set; }
        public DateTime? ProjectStop { get; set; }
        public string Chief { get; set; }
        public decimal? Cost { get; set; }
        public string Inn { get; set; }

        public CustomerInfo InnNavigation { get; set; }
        public ICollection<EmployeeInfo> EmployeeInfo { get; set; }
        public ICollection<ParticipationInProject> ParticipationInProject { get; set; }
    }
}
