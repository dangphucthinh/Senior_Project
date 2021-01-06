using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO.Address
{
    public class HospitalDTO
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string Name { get; set; }
    }
}