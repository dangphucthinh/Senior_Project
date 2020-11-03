using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO;
using Doctor_Appointment.Models.DTO.Appointment;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;


namespace Doctor_Appointment.Repository
{
    public class AppointmentReturnModel
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string Issue { get; set; }
        public DateTime MeetingTime { get; set; }
        public string Detail { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }
        public string StatusName { get; set; }
        public string StartTime { get; set; }

    }

    public class AppoinmentRepository
    {
        public ApplicationDbContext db;
        public AppoinmentRepository()
        {
            this.db = new ApplicationDbContext();
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await db.appointments.ToListAsync();
        }

        public async Task<AppointmentReturnModel> Create(int Id,MakeAppointment model)
        {
            var appointmentInDb = db.appointments.FirstOrDefault(x => (x.DoctorId == model.DoctorId && x.MeetingTime == x.MeetingTime && x.StartTime == model.StartTime));
            
            if(appointmentInDb == null)
            {
                Appointment newApp = new Appointment()
                {
                    Id = Id,
                    MeetingTime = model.MeetingTime,
                    DoctorId = model.DoctorId,
                    Issue = model.Issue,
                    Detail = model.Detail,
                    StatusId = 1,
                    PatientId = model.PatientId,
                    StartTime = model.StartTime
                };
                Appointment app = db.appointments.Add(newApp);

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }


                return new AppointmentReturnModel()
                {
                    DoctorId = app.DoctorId,
                    PatientId = app.PatientId,
                    Id = app.Id,
                    DoctorName = (from user in db.Users where user.Id == model.DoctorId.Trim() select new { FullName = user.FirstName + user.LastName }).FirstOrDefault().FullName,
                    //DoctorName = db.Users.Find(model.DoctorId).FirstName + " " + db.Users.Find(model.DoctorId).LastName,
                    Issue = model.Issue,
                    Detail = model.Detail,
                    MeetingTime = model.MeetingTime,
                    PatientName = (from user in db.Users where user.Id == model.PatientId.Trim() select new { FullName = user.FirstName + user.LastName }).FirstOrDefault().FullName,
                    StatusName = db.appointmentStatuses.Find(model.StatusName).Name
                };
            }

            return null;

        }

        public async Task<AppointmentReturnModel> UpdateInfoAppoinment(UpdateAppoinment model)
        {
           
            Appointment appointment = db.appointments.Find(model.Id);
            PropertyCopier<UpdateAppoinment, Appointment>.Copy(model, appointment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
           
            return null;
        }

        public async Task<IEnumerable<AppointmentReturnModel>> GetAppointmentByUser(string UserId)
        {
            List<AppointmentReturnModel> ret = await (from app in db.appointments
                                                      join status in db.appointmentStatuses on app.StatusId equals status.Id
                                                      join patient in db.patients on app.PatientId equals patient.UserId
                                                      join doctor in db.doctors on app.DoctorId equals doctor.UserId
                                                      where app.PatientId == UserId || app.DoctorId == UserId
                                                      select new AppointmentReturnModel()
                                                      {
                                                          Id = app.Id,
                                                          Issue = app.Issue,
                                                          Detail = app.Detail,
                                                          DoctorId = doctor.UserId,
                                                          PatientId = patient.UserId,
                                                          StartTime = app.StartTime,
                                                          DoctorName = (from user in db.Users where user.Id == doctor.UserId.Trim() select new { FullName = user.FirstName + user.LastName }).FirstOrDefault().FullName,
                                                          PatientName = (from user in db.Users where user.Id == patient.UserId.Trim() select new { FullName = user.FirstName + " " + user.LastName }).FirstOrDefault().FullName,
                                                          StatusName = status.Name
                                                      }).ToListAsync<AppointmentReturnModel>();
                                                      var a = ret;
            return a;
        }
    }
}