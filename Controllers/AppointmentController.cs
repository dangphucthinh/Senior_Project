using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO;
using Doctor_Appointment.Models.DTO.Appointment;
using Doctor_Appointment.Models.DTO.Doctor;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
   // [Authorize]
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
            if (appointment != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,
                    //data = TheModelFactory.GetUser(user)
                    data = appointment
                });
            }
            return Ok(new Response
            {
                status = 1,
                message = ResponseMessages.False,
                //data = TheModelFactory.GetUser(user)
                data = "Cannot make an appointment this time"
            });
            
        }


        [Route("Update")]
        [HttpPost]
        public async Task<IHttpActionResult> Update(UpdateAppoinment model)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = "false",
                    data = ModelState
                });
            }

            await new AppoinmentRepository().UpdateInfoAppoinment(model);

            if (model != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = model
                });
            }
            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = model
            });
        }

        [Route("GetAppoinmentByUser")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetDoctorInfoBySpecialty(PostUserIdModel model)
        {
            IEnumerable<AppointmentReturnModel> app = await new AppoinmentRepository().GetAppointmentByUser(model.UserId);
            if (app != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = app
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = app
            });
        }
    }
}
