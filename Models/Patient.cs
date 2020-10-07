using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string MedicalHistory { get; set; }
        public string Sympton { get; set; }
        public string Allergy { get; set; }
    }
}