using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ITCompany.Models
{
    public partial class ProgrammingLanguages
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public long EmployeeId { get; set; }

        public EmployeeInfo Employee { get; set; }
    }
}
