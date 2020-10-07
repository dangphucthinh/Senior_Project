using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    [RoutePrefix("api/Appointment")]
    public class AppointmentController : BaseAPIController
    {
        [Route("GetAppointmentInfo")]
        public async Task<IHttpActionResult> GetAppointmentInfo()
        {
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = await new AppoinmentRepository().GetAppointments()
            }); 
        }

        [Route("MakeAnAppointment")]
        [HttpPost]
        public async Task<IHttpActionResult> MakeAnAppointment(MakeAppointment makeAppointment)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = ResponseMessages.False,
                    data = ModelState
                });
            }

            var app = new Appointment()
            {
                Issue = makeAppointment.Issue,
                Detail = makeAppointment.Detail,
                StatusId = 1,
                MeetingTime = makeAppointment.MeetingTime
            };

            AppointmentReturnModel appointment = await new AppoinmentRepository().Create(app.Id,makeAppointment);
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                //data = TheModelFactory.GetUser(user)
                data = appointment
            });
        }
    }
}
