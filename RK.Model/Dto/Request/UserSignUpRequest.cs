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
        public string WeChatOpenId { get; set; }
        public string QQOpenId { get; set; }
        public string WeiboOpenId { get; set; }
    }
}
