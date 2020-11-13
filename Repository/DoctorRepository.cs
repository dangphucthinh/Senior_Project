using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using System.IO;
using CloudinaryDotNet.Actions;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class DoctorReturnModel
    {
        //public string Url { get; set; }
        public string Id { get; set; }
        public string Avatar { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool isPatient { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public IList<string> Roles { get; set; }

        //doctor:
        public int DoctorId { get; set; }
        public string UserId { get; set; }
        public string Certification { get; set; }
        public string Education { get; set; }
        public string Bio { get; set; }
        public int Hospital_Id { get; set; }
        public int Specialty_Id { get; set; }
        public string SpecialtyName { get; set; }
        public int HospitalSpecialty_Id { get; set; }
        public string HospitalSpecialty_Name { get; set; }
    }
    public class DoctorRepository
    {
        public ApplicationDbContext db;

        public DoctorRepository()
        {
            this.db = new ApplicationDbContext();
        }
        public async Task<DoctorReturnModel> CreateDoctor(string UserId, DoctorRegister model)
        {
            Doctor newDoc = new Doctor()
            {
                UserId = UserId,
                Specialty_Id = model.Specialty_Id,
                Education = model.Education,
                Hospital_Id = model.Hospital_Id,
                Certification = model.Certification,
                Bio = model.Bio,
                HospitalSpecialty_Id = model.HospitalSpecialty_Id
            };
            Doctor doc = db.doctors.Add(newDoc);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }

            ApplicationUser user = db.Users.Find(UserId);
           
            return new DoctorReturnModel()
            {

                Id = UserId,
                UserName = user.UserName,
                Avatar = user.Avatar,
                Gender = user.Gender,
                Email = model.Email,
                EmailConfirmed = user.EmailConfirmed,
                isPatient = user.isPatient,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FirstName + " " + user.LastName,
                Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                
                DoctorId = doc.Id,
                UserId = UserId,
                Specialty_Id = model.Specialty_Id,
                Education = model.Education,
                Hospital_Id = model.Hospital_Id,
                SpecialtyName = db.specialties.Find(model.Specialty_Id).Name,
                Certification = model.Certification,
                HospitalSpecialty_Id = model.HospitalSpecialty_Id,
                HospitalSpecialty_Name = db.hospitalSpecialties.Find(model.HospitalSpecialty_Id).Name
            };
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await this.db.doctors.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<DoctorReturnModel>> GetAllDoctorsInfo()
        {
            List<DoctorReturnModel> ret = await (from doc in db.doctors
                                                 join spec in db.specialties on doc.Specialty_Id equals spec.Id
                                                 join user in db.Users on doc.UserId equals user.Id
                                                 join hosSpec in db.hospitalSpecialties on doc.HospitalSpecialty_Id equals hosSpec.Id
                                                 where user.isPatient == false
                                                 select new DoctorReturnModel()
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
                                                     PhoneNumber = user.PhoneNumber,
                                                     FullName = user.FirstName + " " + user.LastName,
                                                     Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                                     //doctor:
                                                     DoctorId = doc.Id,
                                                     Certification = doc.Certification,
                                                     Education = doc.Education,
                                                     Hospital_Id = doc.Hospital_Id,
                                                     SpecialtyName = spec.Name,
                                                     Bio = doc.Bio,
                                                     HospitalSpecialty_Id = doc.HospitalSpecialty_Id,
                                                     HospitalSpecialty_Name = hosSpec.Name,
                                                     UserId = doc.UserId
                                                 }).ToListAsync<DoctorReturnModel>();
            return ret;
        }
        public async Task<Doctor> GetDoctor(int id)
        {
            return await this.db.doctors.FindAsync(id);
        }
        public async Task<DoctorReturnModel> GetDoctorInfo(string userId)
        {
            DoctorReturnModel ret = await (from doc in db.doctors
                                           join spec in db.specialties on doc.Specialty_Id equals spec.Id
                                           join user in db.Users on doc.UserId equals user.Id
                                           join hosSpec in db.hospitalSpecialties on doc.HospitalSpecialty_Id equals hosSpec.Id
                                           where user.isPatient == false && user.Id == userId
                                           select new DoctorReturnModel()
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
                                               PhoneNumber = user.PhoneNumber,
                                               FullName = user.FirstName + " " + user.LastName,
                                               Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                               //doctor:
                                               DoctorId = doc.Id,
                                               Certification = doc.Certification,
                                               Education = doc.Education,
                                               Hospital_Id = doc.Hospital_Id,
                                               Bio = doc.Bio,
                                               SpecialtyName = spec.Name,
                                               HospitalSpecialty_Id = doc.HospitalSpecialty_Id,
                                               HospitalSpecialty_Name = hosSpec.Name,
                                               UserId = doc.UserId
                                           }).FirstOrDefaultAsync();
            return ret;
        }
        public async Task<IEnumerable<DoctorReturnModel>> GetDoctorInfoBySpecialty(int specId)
        {
            List<DoctorReturnModel> ret = await (from doc in db.doctors
                                                 join spec in db.specialties on doc.Specialty_Id equals spec.Id
                                                 join user in db.Users on doc.UserId equals user.Id
                                                 join hosSpec in db.hospitalSpecialties on doc.HospitalSpecialty_Id equals hosSpec.Id
                                                 where user.isPatient == false && doc.HospitalSpecialty_Id == specId
                                                 select new DoctorReturnModel()
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
                                                     PhoneNumber = user.PhoneNumber,
                                                     FullName = user.FirstName + " " + user.LastName,
                                                     Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                                     //doctor:
                                                     DoctorId = doc.Id,
                                                     Certification = doc.Certification,
                                                     Education = doc.Education,
                                                     Hospital_Id = doc.Hospital_Id,
                                                     Bio = doc.Bio,
                                                     SpecialtyName = spec.Name,
                                                     HospitalSpecialty_Id = doc.HospitalSpecialty_Id,
                                                     HospitalSpecialty_Name = hosSpec.Name,
                                                     UserId = doc.UserId
                                                 }).ToListAsync<DoctorReturnModel>();
            return ret;
        }
        public async Task<IEnumerable<DoctorReturnModel>> GetDoctorInfoBySpecialtyName(string specName)
        {
            var specObject = await db.specialties.FirstOrDefaultAsync(p => p.Name == specName);
            List<DoctorReturnModel> ret = await (from doc in db.doctors
                                                 join spec in db.specialties on doc.Specialty_Id equals spec.Id
                                                 join user in db.Users on doc.UserId equals user.Id
                                                 join hosSpec in db.hospitalSpecialties on doc.HospitalSpecialty_Id equals hosSpec.Id
                                                 where user.isPatient == false && doc.HospitalSpecialty_Id == specObject.Id
                                                 select new DoctorReturnModel()
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
                                                     PhoneNumber = user.PhoneNumber,
                                                     FullName = user.FirstName + " " + user.LastName,
                                                     Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                                     //doctor:
                                                     DoctorId = doc.Id,
                                                     Certification = doc.Certification,
                                                     Education = doc.Education,
                                                     Hospital_Id = doc.Hospital_Id,
                                                     Bio = doc.Bio,
                                                     SpecialtyName = spec.Name,
                                                     HospitalSpecialty_Id = doc.HospitalSpecialty_Id,
                                                     HospitalSpecialty_Name = hosSpec.Name,
                                                     UserId = doc.UserId
                                                 }).ToListAsync<DoctorReturnModel>();
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

                uploadResult = _cloudinary.Upload(uploadParams);
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

            var avatar = UploadAndGetImage(context.Request.Files[0]);
            user.Avatar = avatar;

            var doctor = db.doctors.FirstOrDefault(x => x.UserId.Trim() == userId.Trim());
  
            doctor.Certification = context.Request.Form["Certification"];
            doctor.Education = context.Request.Form["Education"];
            doctor.Bio = context.Request.Form["Bio"];

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