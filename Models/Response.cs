using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Doctor_Appointment.Models
{
    //public class Response
    //{
    //    public ResponseReturnModel Success(int code, string message, object data)
    //    {
    //        return new ResponseReturnModel
    //        {
    //            status = code,
    //            message = message,
    //            data = data
    //        };
    //    }
    //}

    public class Response 
    {
        public int status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
