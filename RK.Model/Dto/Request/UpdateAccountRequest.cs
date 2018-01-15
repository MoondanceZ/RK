using System;
using System.Collections.Generic;
using System.Text;

namespace RK.Model.Dto.Request
{
    public class UpdateAccountRequest : AccountRequest
    {
        public int Id { get; set; }
    }
}
