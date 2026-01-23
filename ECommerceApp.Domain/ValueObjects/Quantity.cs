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

        private Quantity(int value)
        {
            if (value <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.", nameof(value));
            }
            Value = value;
        }

        public static Quantity FromInt(int value) => new Quantity(value);

        public Quantity Add(Quantity q) => new Quantity(Value + q.Value);
    }
}
