using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO.Appointment
{
    public class UpdateAppoinment
    {
        public int Id { get; set; }
        public string Issue { get; set; }
        public string Detail { get; set; }
        public int StatusId { get; set; }
    }
}