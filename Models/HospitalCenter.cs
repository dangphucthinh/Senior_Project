using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models
{
    public class HospitalCenter
    {
        [Key]
        public int Id { get; set; }
        public int AddressNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int Address_Id { get; set; }
        public string Name { get; set; }
    }
}