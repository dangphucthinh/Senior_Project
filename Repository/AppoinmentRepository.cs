using Doctor_Appointment.Infrastucture;
using Doctor_Appointment.Models;
using Doctor_Appointment.Models.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Doctor_Appointment.Repository
{
    public class AppointmentReturnModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Issue { get; set; }
        public DateTime MeetingTime { get; set; }
        public string Detail { get; set; }
        public string DoctorName { get; set; }
        public string PatientName { get; set; }

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
            return await this.db.appointments.ToListAsync();
        }

        public async Task<AppointmentReturnModel> Create(int Id,MakeAppointment model)
        {
            Appointment newApp = new Appointment()
            {
                MeetingTime = model.MeetingTime,
                DoctorId = model.DoctorId,
                Issue = model.Issue,
                Detail = model.Detail,
                StatusId = 1,
                PatientId = 1
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
                DoctorId = app.Id,
                Id = app.Id,
                DoctorName = db.Users.Find(db.doctors.Find(model.DoctorId).UserId).FirstName + " " + db.Users.Find(db.doctors.Find(model.DoctorId).UserId).LastName,
                Issue = model.Issue,
               Detail = model.Detail,
               MeetingTime = model.MeetingTime,
               //PatientName = db.Users.Find(model.PatientId).FirstName + " "+db.Users.Find(model.PatientId).LastName
            };

        }
    }
}