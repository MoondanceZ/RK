using RK.Model.Dto.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class WeChatUserInfoResponse
    {
        public int Id { get; set; }
        public TokenModel Token { get; set; }
    }
}
