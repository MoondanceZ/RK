using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Api.Common.Exceptions
{
    public class WeChatException : Exception
    {
        public WeChatException(string message) : base(message)
        {
        }
    }
}
