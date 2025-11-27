using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.ValueObjects
{
    public record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(value));
            }
            Value = value;
        }

        public static Quantity FromInt(int value) => new Quantity(value);
    }
}
