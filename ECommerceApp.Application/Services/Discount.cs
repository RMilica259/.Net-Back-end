using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Application.Services
{
    public class Discount
    {
        public decimal Calculate(decimal totalAmount, string phoneNumber, DateTime orderTime)
        {
            if (orderTime.Hour < 16 || orderTime.Hour >= 17)
                return 0;

            var lastDigit = phoneNumber.Last();

            decimal percentage;

            if (lastDigit == '0')
                percentage = 0.30m;

            else if (lastDigit == '2' || lastDigit == '4' || lastDigit == '6' || lastDigit == '8')
                percentage = 0.20m;

            else percentage = 0.10m;

            return totalAmount * percentage;
        }
    }
}
