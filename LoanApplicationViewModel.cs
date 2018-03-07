using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMSGroup2.ViewModels
{
    public class LoanApplicationViewModel
    {
        public int ApplicationId { get; set; }
        public string Name { get; set; }
        public decimal  LoanAmount { get; set; }
        public decimal MonthlyIncome { get; set; }
        public Nullable<System.DateTime> DateOfApproval { get;set;}
        public string Status { get; set; }
        
    }
}