using Doctor_Appointment.Infrastucture;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Certification { get; set; }
        public string Education { get; set; }
        public int Specialty_Id { get; set; }
        public int Hospital_Id { get; set; }
        public int HospitalSpecialty_Id { get; set; }
        public string Bio { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser user { get; set; }

        [ForeignKey("Hospital_Id")]
        public virtual HospitalCenter hospitalCenter { get; set; }

        [ForeignKey("Specialty_Id")]
        public virtual Specialty specialty { get; set; }

    }
}