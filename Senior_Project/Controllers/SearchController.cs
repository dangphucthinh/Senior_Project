using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Doctor_Appointment.Models.Search;
using System.Threading.Tasks;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    [RoutePrefix("api/Search")]
    public class SearchController : BaseAPIController
    {
        [AllowAnonymous]
        [Route("Doctor")]
        [HttpPost]
        public async Task<IHttpActionResult> SearchDoctor(SearchModel model)
        {
            var ret = await new DoctorRepository().GetDoctorBySearch(model.searchPhrase);
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = ret
            });
        }

        [Route("HosSpec")]
        [HttpPost]
        public async Task<IHttpActionResult> SearchHosSpec(SearchModel model)
        {
            var ret = await new HospitalSpecialtyRepository().GetHospitalSpecialtyBySearch(model.searchPhrase);
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = ret
            });
        }


    }
}
