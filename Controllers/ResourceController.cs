using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace Doctor_Appointment.Controllers
{
    //[Authorize]
    public class ResourceController : ApiController
    {
        [HttpGet]
        [Authorize(Roles ="C")]
        [Route("api/Resource/GetData")]
        public string GetData()
        {
            return "success";
        }

        [HttpGet]
        [Authorize]
        [Route("api/Resource/GetDataAno")]
        public string GetDataAno()
        {
            return "success anoymous";
        }
    }
}
