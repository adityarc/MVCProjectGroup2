using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PLMSGroup2.ViewModels
{
    public class AssignInspector
    {
        //public AssignInspector()
        //{
        //    officer_id = 0;
        //    role_id = 0;
        //    application_id = 0;
        //    name = "";
        //    email_id = "";
        //}
        public int officer_id { get; set; }
        public int role_id { get; set; }
        public string name { get; set; }
        public string email_id { get; set; }
        public int application_id { get; set; }
    }

    public class InspectorList
    {
        public InspectorList()
        {

        }
        public List<AssignInspector> GetInspectors { get; set; }
    }
}