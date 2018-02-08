using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class WeChatOpenIdResponse
    {
        [JsonProperty(PropertyName = "session_key")]
        public string SessionKey { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "openid")]
        public string OpenId { get; set; }


        #region 返货错误
        [JsonProperty(PropertyName = "errcode")]
        public int ErrCode { get; set; }
        [JsonProperty(PropertyName = "errmsg")]
        public string ErrMsg { get; set; } 
        #endregion
    }    
}
