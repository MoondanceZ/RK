using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        //public string Account { get; set; }  帐号不允许更改
        public string Name { get; set; }
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Sex { get; set; }
    }
}
