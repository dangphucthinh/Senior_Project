using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ModuleForgotPass.Models
{
    public class UserForResetPasswordModel
    {
        public string userId { get; set; }
        public string code { get; set; }
        public string Password { get; set; }
    }
}