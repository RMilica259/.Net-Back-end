using ECommerceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApp.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.ShippingCity)
                .IsRequired();

            builder.Property(x => x.ShippingStreet)
                .IsRequired();

            builder.Property(x => x.ShippingHouseNumber)
                .IsRequired();

            builder.Property(x => x.ShippingZipCode)
                .IsRequired();

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired();

            builder.Property(x => x.DiscountAmount)
                .IsRequired();

            builder.Property(x => x.OrderDate)
                .IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.Items)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
