using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using Doctor_Appointment.Repository;
using Doctor_Appointment.Utils.Constant;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace Doctor_Appointment.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Doctor")]
    public class DoctorController : BaseAPIController
    {
        [Route("GetListAllDoctor")]
        public async Task<IHttpActionResult> GetListAllDoctor()
        {

            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = await new DoctorRepository().GetAllDoctorsInfo()
            });
        }

        [Route("Register")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(DoctorRegister doctorRegister)
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
            //default account register is patient (isPatient == true)
            var doctor = new ApplicationUser()
            {
                UserName = doctorRegister.Username,
                Email = doctorRegister.Email,
                FirstName = doctorRegister.FirstName,
                Gender = doctorRegister.Gender,
                LastName = doctorRegister.LastName,
                DateOfBirth = doctorRegister.DateOfBirth,
                PhoneNumber = doctorRegister.PhoneNumber,
                isPatient = false,
            };


            IdentityResult result = await this.AppUserManager.CreateAsync(doctor, doctorRegister.Password);

            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = "false",
                    data = result
                });
            }
            AppUserManager.AddToRole(doctor.Id, Constant.Constant.DOCTOR);

            DoctorReturnModel res = await new DoctorRepository().CreateDoctor(doctor.Id, doctorRegister);
            

            if (res != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = res
                });
            }
            return Ok(new Response
            {
                status = 1,
                message = "false",
                //data = TheModelFactory.CreateUser(doctor)
            });
        }

        [Route("GetDoctorInfo")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetDoctorInfo(PostUserIdModel model)
        {
            DoctorReturnModel doctor = await new DoctorRepository().GetDoctorInfo(model.UserId);
            if (doctor != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = doctor
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = doctor
            });
        }

        [Route("GetDoctorInfoBySpecialty")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetDoctorInfoBySpecialty(PostHosSpecIdModel model)
        {
            IEnumerable<DoctorReturnModel> doctor = await new DoctorRepository().GetDoctorInfoBySpecialty(model.HosSpecId);
            if (doctor != null)
            {
                return Ok(new Response
                {
                    status = 0,
                    message = "success",
                    data = doctor
                });
            }

            return Ok(new Response
            {
                status = 1,
                message = "false",
                data = doctor
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Update")]
        public async Task<IHttpActionResult> Update()
        {
            new DoctorRepository().UpdateUser(HttpContext.Current);
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