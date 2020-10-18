﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO
{
    public class MakeAppointment
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime MeetingTime { get; set; }
        [Required]
        public string Issue { get; set; }
        [Required]
        public string Detail { get; set; }
    }
}