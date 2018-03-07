using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMSGroup2.ViewModels
{
    public class DisplayAppDet
    {
        public int application_id { get; set; }
        public string name { get; set; }
        public string email_id { get; set; }
        public string gender { get; set; }
        public System.DateTime date_of_birth { get; set; }
        public int age { get; set; }
        public string contact_no { get; set; }
        public string address { get; set; }
        public string pan_no { get; set; }
        public decimal loan_amount { get; set; }
        public decimal monthly_income { get; set; }
        public string company_name { get; set; }
        public string designation { get; set; }
        public string office_address { get; set; }
        public string office_contact_no { get; set; }
        public string office_email { get; set; }
        public bool existing_loan { get; set; }
        public byte[] photo { get; set; }
        public byte[] address_document { get; set; }
        public string status { get; set; }
        public int reg_id { get; set; }



        public int id { get; set; }

        public int officer_id { get; set; }


        
        public int role_id { get; set; }
        public string oname { get; set; }
        public string oemail_id { get; set; }
        public string ocontact_no { get; set; }
        public string username { get; set; }
        public string password { get; set; }


        

    }
}