using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Doctor_Appointment.DTO;
using Doctor_Appointment.DTO.User;
using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Results;

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
            public string phoneNumber { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public IList<string> Roles { get; set; }
            //patient
            public int PatientId { get; set; }
            public string UserId { get; set; }
            public string Medical_History { get; set; }
            public string Symptom { get; set; }
            public string Allergy { get; set; }
        }

        public ApplicationDbContext db;

        public PatientRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<PatientReturnModel> CreatePatient(string UserId, UserForRegisterDTO model)
        {
            Patient newPatient = new Patient()
            {
                UserId = UserId,
                MedicalHistory = model.MedicalHistory,
                Allergy = model.Allergy,
                Symptom = model.Symptom
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
                Symptom = model.Symptom,
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
                                                      Symptom = patient.Symptom,
                                                      UserId = patient.UserId
                                                  }).ToListAsync<PatientReturnModel>();
            return ret;
        }

        public async Task<PatientReturnModel> GetPatientInfo(string userId)
        {
            PatientReturnModel ret = await (from pat in db.patients
                                           join user in db.Users on pat.UserId equals user.Id
                                           where user.isPatient == true && user.Id == userId
                                           select new PatientReturnModel()
                                           {
                                               // user:
                                               Id = user.Id,
                                               Avatar = user.Avatar,
                                               UserName = user.UserName,
                                               Gender = user.Gender,
                                               Email = user.Email,
                                               EmailConfirmed = user.EmailConfirmed,
                                               isPatient = user.isPatient,
                                               DateOfBirth = user.DateOfBirth,
                                               phoneNumber = user.PhoneNumber,
                                               FullName = user.FirstName + " " + user.LastName,
                                               Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                               //doctor:
                                               PatientId = pat.Id,
                                               Medical_History = pat.MedicalHistory,
                                               Allergy = pat.Allergy,
                                               Symptom = pat.Symptom,
                                               UserId = pat.UserId                                      
                                           }).FirstOrDefaultAsync();
            return ret;
        }

        public string UploadAndGetImage(HttpPostedFile file)
        {
            BinaryReader br = new BinaryReader(file.InputStream);
            byte[] ImageBytes = br.ReadBytes((Int32)file.InputStream.Length);

            Account acc = new Account(
                "deh0sqxwl",
                "212524559265538",
                "1p5EO6Mj_IBdALes5ke3wUMMw6w");
            var _cloudinary = new Cloudinary(acc);

            var uploadResult = new ImageUploadResult();


            if (file.ContentLength > 0)
            {
                MemoryStream stream = new MemoryStream(ImageBytes);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                };

                uploadResult =  _cloudinary.Upload(uploadParams);
            }

            return uploadResult.Url.ToString();
        }

        public string UpdateUser(HttpContext context)
        {

            var userId = context.Request.Form["UserId"];

            var user = db.Users.FirstOrDefault(x => x.Id.Trim() == userId.Trim());
            user.FirstName = context.Request.Form["FirstName"];
            user.LastName = context.Request.Form["LastName"];
           // user.Gender = Convert.ToInt32(context.Request.Form["Gender"].Trim()) == 0 ? false : true;
            //user.DateTime = Convert.ToDateTime(context.Request.Form["Date"]);

            var avatar = this.UploadAndGetImage(context.Request.Files[0]);
            user.Avatar = avatar;


            //var patient = db.patients.FirstOrDefault(x => x.UserId.Trim() == userId.Trim());
            //patient.Allergy = context.Request.Form["Allergy"];
            //patient.MedicalHistory = context.Request.Form["MedicalHistory"];
            //patient.Symptom = context.Request.Form["Symptom"]; 

            var patient = db.patients.FirstOrDefault(x => x.UserId.Trim() == userId.Trim());
            patient.Allergy = context.Request.Form["Allergy"];
            patient.MedicalHistory = context.Request.Form["MedicalHistory"];
            patient.Symptom = context.Request.Form["Symptom"];

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return null;
            }

            return "asdads";


        }

    }
}