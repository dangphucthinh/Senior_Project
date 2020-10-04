using Doctor_Appointment.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO.Doctor
{
    public class DoctorForRegisterDTO : UserForRegisterDTO
    {
        [Required]
        public string Certification { get; set; }
        [Required]
        public string Education { get; set; }
        [Required]
        public int Specialty_Id { get; set; }
        [Required]
        public int Hospital_Id { get; set; }
        [Required]
        public int HospitalSpeciality_Id { get; set; }
        
    }
}