//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModuleForgotPass.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Appointment
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public System.DateTime MeetingTime { get; set; }
        public string StartTime { get; set; }
        public string Issue { get; set; }
        public string Detail { get; set; }
        public int StatusId { get; set; }
    }
}
