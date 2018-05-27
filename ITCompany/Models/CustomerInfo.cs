using System;
using System.Collections.Generic;

namespace ITCompany.Models
{
    public partial class CustomerInfo
    {
        public CustomerInfo()
        {
            ProjectInfo = new HashSet<ProjectInfo>();
        }

        public string Customer { get; set; }
        public string Phone { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string Inn { get; set; }
        public string AdressCost { get; set; }
        public string Fioworker { get; set; }
        public string PhoneWorker { get; set; }

        public ICollection<ProjectInfo> ProjectInfo { get; set; }
    }
}
