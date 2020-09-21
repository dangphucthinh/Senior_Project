using Doctor_Appointment.DTO;
using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    //[RoutePrefix("api/Account")]
    public class AccountController : BaseAPIController
    {
        //Post api/Account/Register
        [Route("api/Auth/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result;
            var user = new ApplicationUser() { UserName = userForRegisterDTO.Username, JoinDate = DateTime.UtcNow };
            try
            {
                result = await this.AppUserManager.CreateAsync(user, userForRegisterDTO.Password);

            }
            catch (Exception ex)
            {
                result = null;
            }
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
           
            return Ok(new
            {
                user.UserName
            });
        }
     
        //Post api/Auth/ChangePassword
        [Authorize]
        [Route("api/Auth/ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(UserForChangePasswordDTO userForChangePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.Name, userForChangePasswordDTO.OldPassword, userForChangePasswordDTO.NewPassword);
            
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok(new
            {
                Notification = "Change passsword succeed"
            }) ;
        }

       
    }
}