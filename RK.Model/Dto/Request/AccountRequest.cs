using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class AccountRequest
    {
        public int UserId { get; set; }
        public int Type { get; set; }
        public int AccountTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string AccountDate { get; set; }
    }
}
