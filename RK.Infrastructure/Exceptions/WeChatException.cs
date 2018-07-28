using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RK.Infrastructure.Exceptions
{
    public class WeChatException : Exception
    {
        public WeChatException(string message) : base(message)
        {
        }
    }
}
