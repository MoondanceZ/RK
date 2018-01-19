using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class AccountPageListRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int UserId { get; set; }
    }
}
