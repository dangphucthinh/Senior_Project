using Doctor_Appointment.App_Start;
using Doctor_Appointment.DTO;
using Doctor_Appointment.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
    //[RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        //private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        //Post api/Account/Register
        [Route("api/Account/Register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(UserForRegisterDTO userForRegisterDTO)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser() { UserName = userForRegisterDTO.Username };
            IdentityResult result = await UserManager.CreateAsync(user, userForRegisterDTO.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
           
            return Ok(new
            {
                user.UserName
            });
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if(result == null)
            {
                return InternalServerError();
            }           
                if (!result.Succeeded)
                {
                    if (result.Errors != null)
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    if (ModelState.IsValid)
                    {
                        // No ModelState errors are available to send, so just return an empty BadRequest.
                        return BadRequest();
                    }

                    return BadRequest(ModelState);
                }

                return null;
        }      


        //Post api/Account/Login
        [Route("api/Account/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(UserForLoginDTO userForLoginDTO)
        {

        
            UserForLoginDTO loginrequest = new UserForLoginDTO { };
            loginrequest.Username = userForLoginDTO.Username.ToLower();
            loginrequest.Password = userForLoginDTO.Password;


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindByNameAsync(userForLoginDTO.Username);

            var checkPassword = await UserManager.CheckPasswordAsync(user,userForLoginDTO.Password);

           string access_token = JwtManager.GenerateToken1(userForLoginDTO.Username);
            if (user == null)
            {
                return Content(HttpStatusCode.NotFound, "Ivalid Username");
            }

            if (!checkPassword)
            {
                return Content(HttpStatusCode.NotFound, "Ivalid Password");
            }



            return Ok(new
            {
                Username = user.UserName,
                token = access_token
            });
        }

        [Authorize]
        [HttpPost]
        [Route("api/Account/ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(UserForChangePasswordDTO userForChangePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var user = await UserManager.FindByNameAsync(userForLoginDTO.Username);

            IdentityResult result = await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), userForChangePasswordDTO.OldPassword, userForChangePasswordDTO.NewPassword);

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