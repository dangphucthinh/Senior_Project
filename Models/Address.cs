using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public int Address_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}