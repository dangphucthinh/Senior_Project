﻿using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO.Doctor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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
        public DateTime DateOfBirth { get; set; }
        public IList<string> Roles { get; set; }
        //doctor:
        public int DoctorId { get; set; }
        public string UserId { get; set; }
        public string Certification { get; set; }
        public string Education { get; set; }
        public int Hospital_Id { get; set; }
        public int Specialty_Id { get; set; }
        public string SpecialityName { get; set; }
        public int HospitalSpeciality_Id { get; set; }
        public string HospitalSpeciality_Name { get; set; }
    }
    public class DoctorRepository
    {
        public ApplicationDbContext db;

        public DoctorRepository()
        {
            this.db = new ApplicationDbContext();
        }
        public async Task<DoctorReturnModel> CreateDoctor(string UserId, DoctorForRegisterDTO model)
        {
            Doctor newDoc = new Doctor()
            {
                UserId = UserId,
                Specialty_Id = model.Specialty_Id,
                Education = model.Education,
                Hospital_Id = model.Hospital_Id,
                Certification = model.Certification,
                HospitalSpeciality_Id = model.HospitalSpeciality_Id
            };
            Doctor doc = db.doctors.Add(newDoc);
            try
            {
                await db.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return null;
            }

            ApplicationUser user = db.Users.Find(UserId);
            return new DoctorReturnModel() {

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

                Specialty_Id = model.Specialty_Id,
                Education = model.Education,
                Hospital_Id = model.Hospital_Id,
                SpecialityName = db.specialties.Find(model.Specialty_Id).Name,
                Certification = model.Certification,
                HospitalSpeciality_Id = model.HospitalSpeciality_Id,
                HospitalSpeciality_Name = db.hospitalSpecialities.Find(model.HospitalSpeciality_Id).Name
            };
        }
        public async Task<IEnumerable<Doctor>> GetAllDoctors()
        {
            return await this.db.doctors.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DoctorReturnModel>> GetDoctorsInfoBySpeciallity(int specId)
        {
            //return 
            List<DoctorReturnModel> ret = await (from doc in db.doctors
                                                 join spec in db.specialties on doc.Specialty_Id equals spec.Id
                                                 join user in db.Users on doc.UserId equals user.Id
                                                 join hosSpec in db.hospitalSpecialities on doc.HospitalSpeciality_Id equals hosSpec.Id
                                                 where user.isPatient == false && doc.HospitalSpeciality_Id == specId
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
                                                     FullName = user.FirstName + " " + user.LastName,
                                                     Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                                     //doctor:
                                                     DoctorId = doc.Id,
                                                     Certification = doc.Certification,
                                                     Education = doc.Education,
                                                     Hospital_Id = doc.Hospital_Id,
                                                     SpecialityName = spec.Name,
                                                     HospitalSpeciality_Id = doc.HospitalSpeciality_Id,
                                                     HospitalSpeciality_Name = hosSpec.Name,
                                                     UserId = doc.UserId
                                                 }).ToListAsync<DoctorReturnModel>();
            return ret;
        }
        public async Task<IEnumerable<DoctorReturnModel>> GetAllDoctorsInfo()
        {
            List<DoctorReturnModel> ret =  await (from doc in db.doctors
                       join spec in db.specialties on doc.Specialty_Id equals spec.Id
                       join user in db.Users on doc.UserId equals user.Id
                       join hosSpec in db.hospitalSpecialities on doc.HospitalSpeciality_Id equals hosSpec.Id
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
                            isPatient  = user.isPatient,
                            DateOfBirth = user.DateOfBirth,
                            FullName = user.FirstName+ " "+user.LastName,
                            Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                            //doctor:
                            DoctorId = doc.Id,
                            Certification = doc.Certification,
                            Education = doc.Education,
                            Hospital_Id = doc.Hospital_Id,
                            SpecialityName = spec.Name,
                            HospitalSpeciality_Id = doc.HospitalSpeciality_Id,
                            HospitalSpeciality_Name = hosSpec.Name,
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
                                    join hosSpec in db.hospitalSpecialities on doc.HospitalSpeciality_Id equals hosSpec.Id
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
                                         FullName = user.FirstName + " " + user.LastName,
                                         Roles = (from ur in user.Roles join rd in db.Roles on ur.RoleId equals rd.Id select rd.Name).ToList<string>(),
                                         //doctor:
                                         DoctorId = doc.Id,
                                         Certification = doc.Certification,
                                         Education = doc.Education,
                                         Hospital_Id = doc.Hospital_Id,
                                         SpecialityName = spec.Name,
                                         HospitalSpeciality_Id = doc.HospitalSpeciality_Id,
                                         HospitalSpeciality_Name = hosSpec.Name,
                                         UserId = doc.UserId
                                     }).FirstOrDefaultAsync();
            return ret;
        }
    }
}