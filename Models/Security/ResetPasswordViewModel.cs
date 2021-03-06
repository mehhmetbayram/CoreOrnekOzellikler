using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjeCoreOrnekOzellikler.Models.Security
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }
        //public string EMail { get; set; }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
