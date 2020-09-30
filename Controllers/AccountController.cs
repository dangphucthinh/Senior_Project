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
            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u))
            });
        }


        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = this.TheModelFactory.Create(user)
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = user
            });

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {  
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = this.TheModelFactory.Create(user)
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = user
            }) ;

        }

        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            //Only SuperAdmin or Admin can delete users

            var appUser = await this.AppUserManager.FindByIdAsync(id);

            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

                if (!result.Succeeded)
                {
                    return Ok(new Response
                    {
                        status = 1,
                        message = "false",
                        data = result
                    });
                }

                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = appUser
                });

            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = appUser
            }); 

        }

        //Post api/Account/Register
        [Route("Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserForRegisterDTO userForRegisterDTO)
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

            var user = new ApplicationUser() {
                UserName = userForRegisterDTO.Username, 
                Email = userForRegisterDTO.Email,
                FirstName = userForRegisterDTO.FirstName,
                LastName = userForRegisterDTO.LastName,
                DateOfBirth = userForRegisterDTO.DateOfBirth,
                isPatient = userForRegisterDTO.isPatient,

            };


            IdentityResult result = await this.AppUserManager.CreateAsync(user, userForRegisterDTO.Password);
            
            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = "false",
                    data = result
                });
            }

            AppUserManager.AddToRole(user.Id, "Patient");

            return Ok( new Response
            {
                status = 0,
                message = "success",
                data = TheModelFactory.Create(user)
            });
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
                return Ok(new Response
                {
                    status = 1,
                    message = "false",
                    data = result
                });
            }

            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = new object()
            });
        }     
    }
}