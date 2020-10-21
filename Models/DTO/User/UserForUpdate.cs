using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.Models.DTO.User
{
    public class UserForUpdate
    {
        public string FirstName { get; set; }

        //public string LastName { get; set; }

        //public bool Gender { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        //public DateTime DateOfBirth { get; set; }

        //public string PhoneNumber { get; set; }

        //public string MedicalHistory { get; set; }
        //public string Symptom { get; set; }
        //public string Allergy { get; set; }

        public HttpPostedFile httpPostedFile { get; set; }
    }
}