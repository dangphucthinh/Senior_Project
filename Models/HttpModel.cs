using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO.Doctor
{
    public class PostUserIdModel
    {
      public string UserId { get; set; }

    }
    public class PostHosSpecIdModel
    {
        public string HosSpecName { get; set; }
    }

    public class PostHsIdModel
    {
        public int HsId { get; set; }
    }

    public class PostAppoinmentIdModel
    {
        public int Id { get; set; }
    }

    public class PostAppointmentId
    {
        public int StatusId { get; set; }
        public string UserId { get; set; }
    }
    public class PostHospitalIdModel
    {
        public int Id { get; set; }
    }
}