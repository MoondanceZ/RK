using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class UserLoginRequest
    {
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
