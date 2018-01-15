using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class AccountResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public int AccountTypeId { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public string AccountDate { get; set; }
    }
}
