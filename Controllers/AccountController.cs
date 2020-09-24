using Doctor_Appointment.DTO;
using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    [RoutePrefix("api/Auth")]


    public class AccountController : BaseAPIController
    {

        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }


        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        //Post api/Account/Register
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IdentityResult result;
            var user = new ApplicationUser() {
                UserName = userForRegisterDTO.Username,
                Email = userForRegisterDTO.Email,
                FirstName = userForRegisterDTO.FirstName,
                LastName = userForRegisterDTO.LastName,
                JoinDate = DateTime.Now.Date,
            };
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

            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, TheModelFactory.Create(user));
        }
     
        //Post api/Auth/ChangePassword
        [Authorize]
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IHttpActionResult> ChangePassword(UserForChangePasswordDTO userForChangePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), userForChangePasswordDTO.OldPassword, userForChangePasswordDTO.NewPassword);
            
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