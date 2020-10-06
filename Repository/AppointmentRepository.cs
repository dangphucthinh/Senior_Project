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
    public class AppointmentRepository
    {
        public ApplicationDbContext db;
        public AppointmentRepository()
        {
            this.db = new ApplicationDbContext();
        }
       
        public IEnumerable<Appointment> GetAppointmentsByPatient()
        {
            return db.appointments
                .Include(p => p.PatientId)
                .Include(d => d.DoctorId)
                .ToList();
        }
    }
}