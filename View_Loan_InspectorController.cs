using PLMSGroup2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace PLMSGroup2.Controllers
{
    public class View_Loan_InspectorController : Controller
    {
        PLMSEntities db = new PLMSEntities();
        // GET: View_Loan_Inspector
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewLoanApplicationLink()//has a view
        {
            return View();
        }
        //public ActionResult LoanInspectorDetailsLink()//has a view
        //{
        //    return View();
        //}
        public ActionResult DisplayNotApprovedLoans(int id)//has a view
        {

            var loans = (from loa in db.loan_officer_assign where loa.officer_id == id select loa).ToList();

            return View(loans);
        }
        public ActionResult SanctionRejectLoan(int id)//has a view
        {
            var q = db.loan_application.Find(id);


            return View(q);
        }


        public ActionResult ApprovedOrRejected(int id)
        {
            
            if (ModelState.IsValid)
            {
                loan_officer_assign loa = new loan_officer_assign();
                loa = db.loan_officer_assign.SingleOrDefault(p => p.application_id == id);
                //var Applicant = db.loan_application.Find(id);
                //string strDDLValue = Request.Form["ddldtatus"].ToString();
                //Applicant.status = strDDLValue;
                //db.SaveChanges();

                //loan_app_or_rej lp = new loan_app_or_rej();
                //lp.date_of_approval = (DateTime)DateTime.Now;
                //lp.application_id = Applicant.application_id;
                //lp.amount_sanctioned = Applicant.loan_amount;
                //lp.status = strDDLValue;
                //lp.interest_rate = 7.50;
                //lp.reasons = "Wont tell you";
                //db.loan_app_or_rej.Add(lp);
                //db.SaveChanges();

                var Applicant = db.loan_application.Find(id);
                string strDDLValue = Request.Form["ddldtatus"].ToString();
                Applicant.status = strDDLValue;
                db.SaveChanges();

                loan_app_or_rej lp = new loan_app_or_rej();
                lp.date_of_approval = (DateTime)DateTime.Now;
                lp.application_id = Applicant.application_id;
                lp.amount_sanctioned = Applicant.loan_amount;
                lp.status = strDDLValue;
                lp.interest_rate = 7.50;
                lp.reasons = "Wont tell you";
                db.loan_app_or_rej.Add(lp);
                db.SaveChanges();

                officer O1 = db.officers.Find(loa.officer_id);
                string EID = O1.email_id;
                string OfficerName = O1.name;
                string AppID = Applicant.application_id.ToString();
                SendVerificationLinkEmailToOfficer(EID, OfficerName, AppID, lp.status);
                return RedirectToAction("ApplicationList", "View_Loan_Applications");
            }
            return RedirectToAction("ApplicationList", "View_Loan_Applications");

            // return View();
        }
        
        public void SendVerificationLinkEmailToOfficer(string emailID, string name, string AID, string status)
        {
            var fromEmail = new MailAddress("plmschennai@gmail.com", "PLMS");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "plms1234"; // Replace with actual password
            string subject = AID + " is "+status+ " by "+ name;

            string body = "Hello " + name + "! \n Loan for Application Number Sanctioned " + AID;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        public ActionResult ListOfAssignedApplication()
        {

            return View();
        }
    }
}