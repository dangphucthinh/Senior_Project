using Doctor_Appointment.DTO;
using Doctor_Appointment.DTO.User;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    [RoutePrefix("api/Auth")]
    public class AccountController : BaseAPIController
    {
        [Route("users")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult GetUsers()
        {
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.GetUser(u))
            });
        }

        [Authorize(Roles = "Admin")]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,
                    data = this.TheModelFactory.GetUser(user)
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = ResponseMessages.False,
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
                    message = ResponseMessages.Success,
                    data = this.TheModelFactory.GetUser(user)
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = ResponseMessages.False,
                data = user
            });

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
                    message = ResponseMessages.False,
                    data = ModelState
                });
            }

            var user = new ApplicationUser()
            {
                UserName = userForRegisterDTO.Username,
                Email = userForRegisterDTO.Email,
                FirstName = userForRegisterDTO.FirstName,
                Gender = userForRegisterDTO.Gender,
                LastName = userForRegisterDTO.LastName,
                DateOfBirth = userForRegisterDTO.DateOfBirth,
                isPatient = true,
                PhoneNumber = userForRegisterDTO.PhoneNumber
            };

            IdentityResult result = await this.AppUserManager.CreateAsync(user, userForRegisterDTO.Password);

            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = ResponseMessages.False,
                    data = result
                });
            }

            AppUserManager.AddToRole(user.Id, Constant.Constant.PATIENT);

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = TheModelFactory.GetUser(user)
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
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,
                    data = ModelState
                });
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), userForChangePasswordDTO.OldPassword, userForChangePasswordDTO.NewPassword);

            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = ResponseMessages.False,
                    data = result
                });
            }

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = new object()
            });
        }

        //Post api/Auth/ResetPassword
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(UserForResetPassword userForResetPassword)
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

            var removePassword = await this.AppUserManager.RemovePasswordAsync(User.Identity.GetUserId());
            if (removePassword.Succeeded)
            {
                //Removed Password Success
                var AddPassword = await this.AppUserManager.AddPasswordAsync(User.Identity.GetUserId(), userForResetPassword.NewPassword);
                if (AddPassword.Succeeded)
                {
                    return Ok(new Response
                    {
                        status = 0,
                        message = ResponseMessages.Success,
                        data = AddPassword
                    });
                }
            }

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = ModelState
            });
        }

        //Post api/Auth/Login   
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(UserForLoginDTO userForLoginDTO)
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

            string baseAddress = "https://localhost:44355";
            using (var client = new HttpClient())
            {
                var form = new Dictionary<string, string>
               {
                   {"grant_type", "password"},
                   {"Username", userForLoginDTO.Username},
                   {"Password", userForLoginDTO.Password},
               };
                var tokenResponse = await client.PostAsync(baseAddress + "/Auth/Login", new FormUrlEncodedContent(form));
                var token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;      
                
                if(token.AccessToken == null)
                {
                    return Ok(new Response
                    {
                        status = 1,
                        message = ResponseMessages.False,
                        data = token
                    });
                }
                var user = await this.AppUserManager.FindByNameAsync(userForLoginDTO.Username);       
                    return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.False,
                        data = new
                        {
                            Token = token,
                            User = user
                        }
                    });
            }
        }
    }
}