using Doctor_Appointment.DTO;
using Doctor_Appointment.DTO.User;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class PatientRepository
    {
        public class PatientReturnModel
        {
            //user
            public string Id { get; set; }
            public string Avatar { get; set; }
            public string UserName { get; set; }
            public string FullName { get; set; }
            public bool Gender { get; set; }
            public string Email { get; set; }
            public bool EmailConfirmed { get; set; }
            public bool isPatient { get; set; }
            public DateTime DateOfBirth { get; set; }
            public IList<string> Roles { get; set; }
            //patient
            public int PatientId { get; set; }
            public string UserId { get; set; }
            public string Medical_History { get; set; }
            public string Sympton { get; set; }
            public string Allergy { get; set; }
        }

        public ApplicationDbContext db;

        public PatientRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<Patient>> GetAllPatient()
        {
            return await this.db.patients.AsNoTracking().ToListAsync();
        }

        public async Task<PatientReturnModel> CreatePatient(string UserId, UserForRegisterDTO model)
        {
            Patient newPatient = new Patient()
            {
                UserId = UserId,
                MedicalHistory = model.MedicalHistory,
                Allergy = model.Allergy,
                Sympton = model.Sympton
            };

            Patient patient = db.patients.Add(newPatient);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            ApplicationUser user = db.Users.Find(UserId);
            return new PatientReturnModel()
            {
                Id = UserId,
                UserName = user.UserName,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Email = model.Email,
                EmailConfirmed = user.EmailConfirmed,
                isPatient = user.isPatient,
                DateOfBirth = user.DateOfBirth,
                FullName = user.FirstName + " " + user.LastName,
                Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),

                //patient
                UserId = UserId,
                PatientId = patient.Id,
                Medical_History = model.MedicalHistory,
                Sympton = model.Sympton,
                Allergy = model.Allergy

            };
        }

        public async Task<IEnumerable<PatientReturnModel>> GetAllPatientInfo()
        {
            List<PatientReturnModel> ret = await (from patient in db.patients
                                                  join user in db.Users on patient.UserId equals user.Id
                                                  where user.isPatient == true
                                                  select new PatientReturnModel()
                                                  {
                                                      Id = user.Id,
                                                      Avatar = user.Avatar,
                                                      UserName = user.UserName,
                                                      Gender = user.Gender,
                                                      Email = user.Email,
                                                      EmailConfirmed = user.EmailConfirmed,
                                                      isPatient = user.isPatient,
                                                      DateOfBirth = user.DateOfBirth,
                                                      FullName = user.FirstName + " " + user.LastName,
                                                      Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                                      //doctor:
                                                      PatientId = patient.Id,
                                                      Medical_History = patient.MedicalHistory,
                                                      Allergy = patient.Allergy,
                                                      Sympton = patient.Sympton,
                                                      UserId = patient.UserId
                                                  }).ToListAsync<PatientReturnModel>();
            return ret;
        }
    }
}