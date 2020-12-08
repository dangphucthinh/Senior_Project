using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
   // [Authorize]
    [RoutePrefix("api/Speciality")]
    public class SpecialityController : BaseAPIController
    {
        [Route("GetAllHospitalSpecialities")]
        public async Task<IHttpActionResult> GetAllHospitalSpecialities()
        {
            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = await new HospitalSpecialtyRepository().GetAllHospitalSpecialty()
            });
        }

        [Route("Update")]
        public async Task<IHttpActionResult> Update()
        {
            new HospitalSpecialtyRepository().UpdateImage(HttpContext.Current);
            //var image = HttpContext.Current.Request.Files[0];

            //new PatientRepository().UploadAndGetImage(image);

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = { }
            });
        }


        [Route("GetAllSpecialities")]
        [HttpPost]
        public async Task<IHttpActionResult> GetAllSpecialities(PostHsIdModel model)
        {

            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = await new HospitalSpecialtyRepository().GetAllSpecialties(model.HsId)
            });
        }
    }
}
