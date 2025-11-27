using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Domain.Errors
{
    public class Errors
    {
        public static class Cart
        {
            public static Error ItemAlreadyExists() => new("CARTT001", "The item already exists in the cart.");
            public static Error InsuficientStock() => new("CARTT002", "Insufficient stock for the requested item.");
            public static Error CartIsEmpty() => new("CARTT003", "The cart is empty.");
        }

        public static class Product
        {
            public static Error ProductNotFound() => new("PRODT001", "The requested product was not found.");
        }

        public static class Order
        {
            public static Error FailedToCreate() => new("ORDER001", "Failed to create order");
            public static Error InvalidPhoneNumber() => new("ORDER0002", "Phone number is invalid.");
            public static Error AddressRequired() => new("ORDER0003", "Shipping address is required.");
        }
    }
}
