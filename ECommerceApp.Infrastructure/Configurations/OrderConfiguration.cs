using ECommerceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.TotalAmount).IsRequired();
            builder.Property(e => e.OrderDate).IsRequired();

            builder.OwnsOne(e => e.Address, address =>
            {
                address.Property(e => e.City).IsRequired();
                address.Property(e => e.Street).IsRequired();
                address.Property(e => e.HouseNumber).IsRequired();
                address.Property(e => e.ZipCode).IsRequired();
            });
        }
    }
}
