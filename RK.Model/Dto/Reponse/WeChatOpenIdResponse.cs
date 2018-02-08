using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class WeChatOpenIdResponse
    {
        public string session_key { get; set; }
        public int expires_in { get; set; }
        public string openid { get; set; }


        #region 返货错误
        public int errcode { get; set; }
        public string errmsg { get; set; } 
        #endregion
    }    
}
