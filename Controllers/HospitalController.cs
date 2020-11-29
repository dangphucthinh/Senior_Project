using Doctor_Appointment.Models;
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
    [RoutePrefix("api/Hospital")]
    public class HospitalController : ApiController
    {
        [Route("ListHospitalCenter")]
        public async Task<IHttpActionResult> GetListHospital()
        {
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                //data = await new HospitalRepository().getListHospital()
                data = {}
            });
        }

   
    }
}
