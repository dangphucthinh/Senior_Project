using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HsId { get; set; }
        [ForeignKey("HsId")]
        public virtual HospitalSpecialty hospitalSpecialty { get; set; }
    }
}
