using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PLMSGroup2;
using System.Data.Entity;


namespace PLMSGroup2.Controllers
{
    public class ChangePasswordController : Controller
    {
        private PLMSEntities db = new PLMSEntities();
        // GET: ChangePassword
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePasswordLink()
        {
            return View();
        }

        public ActionResult Validation(int id,registration reg)
        {

           // registration r = db.registrations.Find(id);
            var tuple = db.registrations.SingleOrDefault(p => p.reg_id == id);
            if (tuple.answer == reg.answer)
            {
                return RedirectToAction("SetNewPassword/" + id, "ChangePassword");
            }
            else
            {

                return RedirectToAction("MessageToDisplay");
            }
          
        }
        public ActionResult AnswerSecurityQuestion(int? id)
        
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            var question = db.registrations.SingleOrDefault(p => p.reg_id == id);
            return View(question);
            }
        
        public ActionResult SetNewPassword(int? id)
        {
            //ViewBag.password = new SelectList(db.registrations, "password", "", registration.password);
            //var reg = db.registrations.SingleOrDefault(p => p.reg_id == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var cpassword = db.registrations.SingleOrDefault(p => p.reg_id == id);
            return View(cpassword);

            


        }
        public ActionResult UpdatePassword(int? id, registration r)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registration registration = db.registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            r.reg_id = registration.reg_id;
            r.name = registration.name;
            r.contact_no = registration.contact_no;
            r.gender = registration.gender;
            r.date_of_birth = (DateTime)registration.date_of_birth;
            r.address = registration.address;
            r.username = registration.username;

            r.email_id = registration.email_id;
            r.question = registration.question;
            r.answer = registration.answer;
            r.status = registration.status;





            registration.reg_id = r.reg_id;
            registration.name = r.name;
            registration.contact_no = r.contact_no;
            registration.gender = r.gender;
            registration.date_of_birth = r.date_of_birth;
            registration.address = r.address;
            registration.username = r.username;
            registration.password = r.password;                 //Updation of Password is done here
            registration.email_id = r.email_id;
            registration.question = r.question;
            registration.answer = r.answer;
            registration.status = r.status;
            registration.cpassword = r.cpassword;
            db.SaveChanges();
            return RedirectToAction("ChangePasswordLink");
        }
        public ActionResult MessageToDisplay()
        {
            
            return View();
        }
    }
}