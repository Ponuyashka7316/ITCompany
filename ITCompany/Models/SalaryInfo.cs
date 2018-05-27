using System;
using System.Collections.Generic;

namespace ITCompany.Models
{
    public partial class SalaryInfo
    {
        public decimal? Salary { get; set; }
        public decimal? Bonus { get; set; }
        public decimal? GrossSalary { get; set; }
        public decimal? Exempt { get; set; }
        public long EmployeeId { get; set; }

        public EmployeeInfo EmployeeInfo { get; set; }
    }
}
