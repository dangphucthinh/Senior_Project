using Doctor_Appointment.DTO;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.User;
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
            public string PhoneNumber { get; set; }
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
                Medical_History = model.Medical_History,
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
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = user.EmailConfirmed,
                isPatient = user.isPatient,
                DateOfBirth = user.DateOfBirth,
                FullName = user.FirstName + " " + user.LastName,
                Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),

                //patient
                UserId = UserId,
                PatientId = patient.Id,
                Medical_History = model.Medical_History,
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
                                                      Medical_History = patient.Medical_History,
                                                      Allergy = patient.Allergy,
                                                      Sympton = patient.Sympton,
                                                      UserId = patient.UserId
                                                  }).ToListAsync<PatientReturnModel>();
            return ret;
        }

        public async Task<PatientReturnModel> UpdateInfoPatient(string UserId, UserForUpdate model)
        {
            ApplicationUser user = db.Users.Find(UserId);
            Patient tmp = db.patients.Where(p => p.UserId == UserId).FirstOrDefault();
            Patient patient = tmp != null ? db.patients.Find(tmp.Id) : null;


            PropertyCopier<UserForUpdate, ApplicationUser>.Copy(model, user);
            PropertyCopier<UserForUpdate, Patient>.Copy(model, patient, new List<string>() { "UserId" });

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            PatientReturnModel ret = new PatientReturnModel();
            PropertyCopier<ApplicationUser, PatientReturnModel>.Copy(user, ret);
            PropertyCopier<Patient, PatientReturnModel>.Copy(patient, ret);

            ret.FullName = user.FirstName + " " + user.LastName;
            ret.Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>();

            return ret;
        }
    }
}
