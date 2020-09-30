using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }
    }
}