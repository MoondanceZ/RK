using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Common
{
    public class TokenModel
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Accesstoken { get; set; }
        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }
        [JsonProperty(PropertyName = "refresh_token")]
        public string RefreshToken { get; set; }
    }
}
