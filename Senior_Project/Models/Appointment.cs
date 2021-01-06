using Doctor_Appointment.Infrastucture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public DateTime MeetingTime { get; set; }
        public string StartTime { get; set; }
        public string Issue { get; set; }
        public string Detail { get; set; }
        public int StatusId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual ApplicationUser doctor { get; set; }
        [ForeignKey("PatientId")]
        public virtual ApplicationUser patient { get; set; }
        [ForeignKey("StatusId")]
        public virtual AppointmentStatus appointmentStatus { get; set; }
            
    }
}