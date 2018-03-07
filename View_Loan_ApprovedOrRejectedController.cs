using PLMSGroup2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PLMSGroup2.Controllers
{
    public class View_Loan_ApprovedOrRejectedController : Controller
    {
        PLMSEntities db = new PLMSEntities();
        // GET: View_Loan_ApprovedOrRejected
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowDetailsLink()//has a view
        {
            return View();
        }
        public ActionResult GetLoanApplication()
        {
            return View();
        }



        public ActionResult DiplayApplicationWithStatus()//has a view
        {
            var L1 = db.loan_application;
            var L2 = db.loan_app_or_rej;
            var Q = (from la in L1
                     join ls in L2 on la.application_id equals ls.application_id
                     where ls.status == "approved" || ls.status == "rejected" orderby ls.date_of_approval descending
                     select new
                     {
                         ApplicantID = la.application_id,
                         Name = la.name,
                         LoanAmount = la.loan_amount,
                         MonthlyIncome = la.monthly_income,
                         Status = ls.status,
                         DateOfApproval = ls.date_of_approval

                     }).ToList();
            List<LoanApplicationViewModel> listViewModel = new List<LoanApplicationViewModel>();
            foreach (var item in Q)
            {
                LoanApplicationViewModel viewmodel = new LoanApplicationViewModel();
                viewmodel.ApplicationId = item.ApplicantID;
                viewmodel.Name = item.Name;
                viewmodel.LoanAmount = item.LoanAmount;
                viewmodel.MonthlyIncome = item.MonthlyIncome;
                viewmodel.DateOfApproval = item.DateOfApproval;
                viewmodel.Status = item.Status;
                listViewModel.Add(viewmodel);
            }

            return View(listViewModel);
        }

        public List<loan_application> ShowApplicants()
        {
            return db.loan_application.ToList();
        }
    }
}