﻿using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using Doctor_Appointment.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace Doctor_Appointment.Controllers
{
    [Authorize]
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
        public async Task<IHttpActionResult> Register(DoctorForRegisterDTO doctorForRegisterDTO)
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
                UserName = doctorForRegisterDTO.Username,
                Email = doctorForRegisterDTO.Email,
                FirstName = doctorForRegisterDTO.FirstName,
                Gender = doctorForRegisterDTO.Gender,
                LastName = doctorForRegisterDTO.LastName,
                DateOfBirth = doctorForRegisterDTO.DateOfBirth,
                isPatient = false,              
            };


            IdentityResult result = await this.AppUserManager.CreateAsync(doctor, doctorForRegisterDTO.Password);

            if (!result.Succeeded)
            {
                return Ok(new Response
                {
                    status = 1,
                    message = "false",
                    data = result
                });
            }
            AppUserManager.AddToRole(doctor.Id, "Doctor");

            DoctorReturnModel res = await new DoctorRepository().CreateDoctor(doctor.Id, doctorForRegisterDTO);

            if(res != null)
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
        
        [Route("GetDoctorInfo/{UserId}")]
        public async Task<IHttpActionResult> GetDoctorInfo(string UserId)
        {
           // var doctor = await this.AppUserManager.FindByNameAsync(UserId);

            DoctorReturnModel doctor = await new DoctorRepository().GetDoctorInfo(UserId);
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

        [Route("GetDoctorInfoBySpec/{HosSpecId}")]
        public async Task<IHttpActionResult> GetDoctorInfoBySpec(int HosSpecId)
        {
          IEnumerable <DoctorReturnModel> doctor = await new DoctorRepository().GetDoctorsInfoBySpeciallity(HosSpecId);
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
    }
}
