﻿using Doctor_Appointment.DTO;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.User;
using Doctor_Appointment.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using static Doctor_Appointment.Repository.PatientRepository;

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
                data = this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.CreateUser(u))
            });
        }

        [Route("GetListAllPatient")]
        public async Task<IHttpActionResult> GetListAllPatient()
        {
            return Ok(new Response
            {
                status = 0,
                message = "success",
                data = await new PatientRepository().GetAllPatientInfo()
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
                    data = this.TheModelFactory.CreateUser(user)
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
                    data = this.TheModelFactory.CreateUser(user)
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
            //default account register is patient (isPatient == true)
            var user = new ApplicationUser() {
                UserName = userForRegisterDTO.Username, 
                Email = userForRegisterDTO.Email,
                FirstName = userForRegisterDTO.FirstName,
                LastName = userForRegisterDTO.LastName,
                PhoneNumber = userForRegisterDTO.PhoneNumber,
                DateOfBirth = userForRegisterDTO.DateOfBirth,
                Gender = userForRegisterDTO.Gender,
                isPatient = true,
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
            PatientReturnModel res = await new PatientRepository().CreatePatient(user.Id, userForRegisterDTO);

            return Ok( new Response
            {
                status = 0,
                message = "success",
                //data = TheModelFactory.CreateUser(user)
                data = res
            });
        }

        //Post api/Auth/ChangePassword
        
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
   
        [Route("Update/{UserId}")]
        [HttpPost]
        [Authorize(Roles ="Patient")]
        
        public async Task<IHttpActionResult> Update(string UserId,UserForUpdate userForUpdate)
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
            var patient = new Patient()
            {
               // UserName = userForUpdate.Username,
                Allergy = userForUpdate.Allergy,
                Medical_History = userForUpdate.Medical_History,
                //Gender = userForUpdate.Gender,
                Sympton = userForUpdate.Sympton
                
            };

            //IdentityResult result = this.AppUserManager.Update(patient);
            //if (!result.Succeeded)
            //{
            //    return Ok(new Response
            //    {
            //        status = 1,
            //        message = "false",
            //        data = result
            //    });
            //}

            PatientReturnModel res = await new PatientRepository().UpdateInfoPatient(UserId, userForUpdate);

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
    }
}