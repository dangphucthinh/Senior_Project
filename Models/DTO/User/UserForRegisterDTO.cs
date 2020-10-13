﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Doctor_Appointment.DTO
{
    public class UserForRegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public bool Gender { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public bool isPatient { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Medical_History { get; set; }
        public string Sympton { get; set; }
        public string Allergy { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}