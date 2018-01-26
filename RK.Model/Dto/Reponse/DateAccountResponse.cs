using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Reponse
{
    public class DateAccountResponse
    {
        public string Date { get; set; }
        public string DateAmount { get; set; }
        public string MonthIncome { get; set; }
        public string MonthExpend { get; set; }
        public List<AccountResponse> AccountRecords { get; set; }
    }
}
