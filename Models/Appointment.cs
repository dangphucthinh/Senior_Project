using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime MeetingTime { get; set; }
        public string Issue { get; set; }
        public string Detail { get; set; }
        public int StatusId { get; set; }
        
    }
}