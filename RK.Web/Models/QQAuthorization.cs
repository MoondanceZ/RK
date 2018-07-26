using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Web.Models
{
    public class QcInfo
    {
        public string error { get; set; }
        public string error_description { get; set; }
    }

    public class QcOpenId : QcInfo
    {
        public string client_id { get; set; }
        public string openid { get; set; }
    }

    public class QcToken : QcInfo
    {
        public QcToken(string tokenInfo)
        {
            var tokenInfoAry = tokenInfo.Split('&');
            for (int i = 0; i < tokenInfoAry.Length; i++)
            {
                var temp = tokenInfoAry[i];
                if (temp.StartsWith("access_token"))
                {
                    this.access_token = temp.Split('=')[1];
                }
                else if (temp.StartsWith("expires_in"))
                {
                    this.expires_in = temp.Split('=')[1];
                }
                else if (temp.StartsWith("refresh_token"))
                {
                    this.refresh_token = temp.Split('=')[1];
                }
            }
        }
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
    }

    public class QcUser : QcInfo
    {
        public int ret { get; set; }
        public string msg { get; set; }
        public int is_lost { get; set; }
        public string nickname { get; set; }
        public string gender { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string year { get; set; }
        public string constellation { get; set; }
        public string figureurl { get; set; }
        public string figureurl_1 { get; set; }
        public string figureurl_2 { get; set; }
        public string figureurl_qq_1 { get; set; }
        public string figureurl_qq_2 { get; set; }
        public string is_yellow_vip { get; set; }
        public string vip { get; set; }
        public string yellow_vip_level { get; set; }
        public string level { get; set; }
        public string is_yellow_year_vip { get; set; }
    }

}
