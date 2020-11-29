using Doctor_Appointment.DTO;
using Doctor_Appointment.DTO.User;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using static Doctor_Appointment.Repository.PatientRepository;
using System.Web;
using Doctor_Appointment.Constant;
using Doctor_Appointment.Models.DTO.User;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;

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
        [Authorize(Roles = "Admin")]
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

        //Get api/Auth/GetListAllPatient
        [Route("GetListAllPatient")]
        public async Task<IHttpActionResult> GetListAllPatient()
        {
            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = await new PatientRepository().GetAllPatientInfo()
            });
        }

        //Post api/Auth/Register
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
                //Gender = userForRegisterDTO.Gender,
                LastName = userForRegisterDTO.LastName,
                DateOfBirth = userForRegisterDTO.DateOfBirth != new DateTime() ? userForRegisterDTO.DateOfBirth : DateTime.Today,
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

            string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));

            await this.AppUserManager.SendEmailAsync(user.Id,
                                                    "Confirm your account",
                                                    "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            //Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));
            PatientReturnModel res = await new PatientRepository().CreatePatient(user.Id, userForRegisterDTO);

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = res
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(UserForForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.AppUserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await this.AppUserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return Ok(new Response
                    {
                        status = 1,
                        message = "false, Cannot find user or email is not confirmed!",
                    });
                }

                var code = await this.AppUserManager.GeneratePasswordResetTokenAsync(user.Id);
                //var callbackUrl = new Uri(Url.Link("ResetPasswordEmailRoute", new { userId = user.Id, code = code }));
                //    var callbackUrl = Url.Action("ResetPassword", "Account",
                //new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                var outPutDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string configPath = Path.Combine(outPutDirectory, "Utils\\Constant\\server_config.json");
                StreamReader r = new StreamReader(configPath);
                string json = r.ReadToEnd();
                ServerConfig conf = JsonConvert.DeserializeObject<ServerConfig>(json);

                string mvc_server = conf.mvc_server;
                string userId = HttpUtility.UrlEncode(user.Id);
                string codeUrl = HttpUtility.UrlEncode(code); 
                string callbackUrl = mvc_server+"/Home/ResetPasswordEmail?userId=" + userId + "&code=" + codeUrl; 
                await this.AppUserManager.SendEmailAsync(user.Id, "Reset Password",
            "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,
                    //data = TheModelFactory.GetUser(user)
                });
            }

            // If we got this far, something failed, redisplay form
            return Ok(new Response
            {
                status = 1,
                message = "false, Unknown Error.",

            });
        }
        //Post api/Auth/ResetPassword
        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPasswordEmailPost", Name = "ResetPasswordEmailPostRoute")]
        public async Task<IHttpActionResult> ResetPasswordEmailPost(UserForResetPasswordPost model)
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

            var removePassword = await this.AppUserManager.ResetPasswordAsync(model.userId, model.code, model.Password);
            if (removePassword.Succeeded)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,

                });
            }

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = ModelState
            });
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPasswordEmail", Name = "ResetPasswordEmailRoute")]
        public async Task<IHttpActionResult> ResetPasswordEmail(string userId = "", string code = "")
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

            var removePassword = await this.AppUserManager.ResetPasswordAsync(userId, code, "Abc@12345");
            if (removePassword.Succeeded)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = ResponseMessages.Success,

                });
            }

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = ModelState
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

            string baseAddress = API.HTTP;
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

                if (token.AccessToken == null)
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
                    message = ResponseMessages.Success,
                    data = new
                    {
                        Token = token,
                        User = this.TheModelFactory.GetUser(user)
                    }
                });
            }
        }

        [HttpPost]
        [Route("GetPatientInfo")]
        // [AllowAnonymous]
        public async Task<IHttpActionResult> GetPatientInfo(PostUserIdModel model)
        {
            PatientReturnModel patient = await new PatientRepository().GetPatientInfo(model.UserId);
            if (patient != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = patient
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = patient
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Update")]
        public async Task<IHttpActionResult> Update()
        {
            new PatientRepository().UpdateUser(HttpContext.Current);
            //var image = HttpContext.Current.Request.Files[0];

            //new PatientRepository().UploadAndGetImage(image);

            return Ok(new Response
            {
                status = 0,
                message = ResponseMessages.Success,
                data = { }
            });
        }
    }
}
