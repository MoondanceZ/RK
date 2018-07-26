using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class UserSignUpRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public string Nmae { get; set; }
    }
}
