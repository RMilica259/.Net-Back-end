using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Errors
{
    public class Error
    {
        public string ErrorCode { get; }
        public string ErrorMessage { get; }
        public Error(string errorCode, string errorMessage) {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
