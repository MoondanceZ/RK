using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class UserSignInResponse
    {
        public UserInfoResponse UserInfo { get; set; }
        public TokenModel Token { get; set; }
        public class TokenModel
        {
            public string access_token { get; set; }
            public long expires_in { get; set; }
            public string token_type { get; set; }
            public string refresh_token { get; set; }
        }
    }

}
