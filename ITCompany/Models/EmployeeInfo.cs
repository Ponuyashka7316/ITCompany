using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITCompany.Models
{
    public partial class EmployeeInfo
    {
        public EmployeeInfo()
        {
            ParticipationInProject = new HashSet<ParticipationInProject>();
            ProgrammingLanguages = new HashSet<ProgrammingLanguages>();
        }
        [Key]
        public long EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Adress { get; set; }
        public string District { get; set; }
        public int Experiance { get; set; }
        public DateTime Year { get; set; }
        public string Language { get; set; }
        public string Base { get; set; }
        public string Comment { get; set; }
        public int? BonusAll { get; set; }
        public long? ProjectId { get; set; }

        public SalaryInfo Employee { get; set; }
        public ProjectInfo Project { get; set; }
        public ICollection<ParticipationInProject> ParticipationInProject { get; set; }
        public ICollection<ProgrammingLanguages> ProgrammingLanguages { get; set; }
    }
}
