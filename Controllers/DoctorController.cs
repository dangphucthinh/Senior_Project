using Doctor_Appointment.Constant;
using Doctor_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Security;

namespace Doctor_Appointment.Controllers
{
    public class DoctorController : BaseAPIController
    {
        public Doctor doctor;

        [HttpPost]
        [Authorize(Roles = Constant.Constant.PATIENT)]
        [Route("api/Resource/GetData")]
        public IHttpActionResult getListDoctor()
        {
       
            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = this.AppUserManager.Users.ToList().Select(u => u.Roles.ToList())
            });
        }


    }
}
