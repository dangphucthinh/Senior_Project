using Doctor_Appointment.Models;
using Doctor_Appointment.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [RoutePrefix("api/Appointment")]
    [Authorize]
    public class AppointmentController : BaseAPIController
    {
        [Route("GetAppointmentByPatient")]
        public IHttpActionResult GetAppointMentByPatient()
        {
            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = new AppointmentRepository().GetAppointmentsByPatient()
            });
        }
    }
}
