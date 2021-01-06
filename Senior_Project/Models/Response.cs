using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace Doctor_Appointment.Models
{
    public class Response
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
   
    }
}