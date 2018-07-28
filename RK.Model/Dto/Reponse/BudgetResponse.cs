using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class BudgetResponse
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Income { get; set; }
        public string Expend { get; set; }
        public string LastMonthExpend { get; set; }
        public List<LastMonthExpendResponse> LastMonthTop3Expend { get; set; }
        public class LastMonthExpendResponse
        {
            public string Expend { get; set; }
            public double ExpendPercent { get; set; }
            public string TypeCode { get; set; }
            public string TypeName { get; set; }
        }
    }
}
