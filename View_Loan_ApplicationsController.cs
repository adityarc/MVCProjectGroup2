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
    public class View_Loan_ApplicationsController : Controller
    {
        private PLMSEntities db = new PLMSEntities();
        // GET: View_Loan_Applications
        //public ActionResult Index()
        //{
        //    return RedirectToAction("ViewLoanApplicationLink");
        //}
        public ActionResult ViewLoanApplicationLink()//has view
        {
            return View();
        }
        public ActionResult ApplicationList()//has view
        {
            //var ApplicationList = GetApplicants();

            var L1 = db.loan_application;
            var L2 = db.loan_officer_assign;
            var L3 = db.officers;


            var det = from pro in L1
                      where pro.status == "pending" || pro.status == "rejected" 
                      select pro;

            return View(det);
            //var det = db.loan_application.SingleOrDefault(aid => aid.application_id == id);
            
            //return View(ApplicationList);
        }
        public List<loan_application> GetApplicants()
        {
            return db.loan_application.ToList();
        }

        public ActionResult GetApplicationDetails()
        {
            return View();
        }
        public ActionResult DisplayApplicationDetails(int? id)//has view

        {
            //var L1 = db.loan_application;
            //var L2 = db.loan_officer_assign;
            //var L3 = db.officers;
            //var det = from pro in L1
            //          join ord in L2 on pro.application_id equals ord.application_id
            //          where ord.officer_id != 0 && pro.status == "pending"
            //          select pro;
                    
            var det = db.loan_application.SingleOrDefault(aid => aid.application_id == id);
            return View(det);
            
        }
        public ActionResult AssignApplication(int id)//has view here the id belongs to the application ID.
        {
            var L1 = db.officers;
            var L2 = db.roles;

            var det1 = from o in L1
                      join r in L2 on o.role_id equals r.role_id
                      where r.role_name == "Loan Inspector"
                      select new
                      {
                          officer_id = o.officer_id,
                          role_id = o.role_id,
                          name = o.name,
                          email_id = o.email_id,
                          application_id = id
                      };
                      

            List<AssignInspector> det = new List<AssignInspector>();
            
            
            foreach ( var k in det1)
            {

                AssignInspector temp = new AssignInspector();
                temp.officer_id = k.officer_id;
                temp.role_id = k.role_id;
                temp.name = k.name;
                temp.email_id = k.email_id;
                temp.application_id = k.application_id;

                det.Add(temp);    
            }
            
            
            return View(det);
        }
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string name, string AID)
        {
            var fromEmail = new MailAddress("plmschennai@gmail.com", "PLMS");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "plms1234"; // Replace with actual password
            string subject = AID + " is successfully assigned!";

            string body = "Hello " + name + "! \n You have been assigned to review Application Number "+AID;

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

        public ActionResult AssignInspector(int id, int sender)
        {
            //var lor = db.loan_officer_assign.Add(AI)
            // OfficerAssign OA = new OfficerAssign();
            //var AI = AI1.Find(p => p.officer_id == id);
            loan_officer_assign oa = new loan_officer_assign();
            
            oa.application_id = sender;
            oa.officer_id = id;
            db.loan_officer_assign.Add(oa);
            db.SaveChanges();

            loan_application la = new loan_application();
            la = db.loan_application.Find(sender);
            //if(la == null)
            //{
            //    return HttpNotFound();
            //}
            la.status = "Assigned";
            db.SaveChanges();
            officer O1 = db.officers.Find(id);
            string EID = O1.email_id;
            string OfficerName = O1.name;
            string AppID =  oa.application_id.ToString();
            SendVerificationLinkEmail(EID, OfficerName, AppID);
            return RedirectToAction("ApplicationList","View_Loan_Applications");
        }
        public ActionResult Message()
        {
            return View();
        }
        
    }
}