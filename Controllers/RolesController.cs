using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Security;

namespace Doctor_Appointment.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/roles")]
    public class RolesController : BaseAPIController
    {
        [Route("{id:guid}", Name = "GetRoleById")]
        public async Task<IHttpActionResult> GetRole(string Id)
        {
            var role = await this.AppRoleManager.FindByIdAsync(Id);

            if(role != null)
            {
                return Ok();
            }
            return NotFound();
        }

        [Route("", Name = "GetAllRoles")]
        public IHttpActionResult GetAllRoles()
        {
            var roles = this.AppRoleManager.Roles;
            return Ok(roles);
        }


        //[Route("create")]
        //public async Task<IHttpActionResult> Create()
        //{

        //}
    }
}
