using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class AddAccountRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public string Remark { get; set; }
        public DateTime Time { get; set; }
    }
}
