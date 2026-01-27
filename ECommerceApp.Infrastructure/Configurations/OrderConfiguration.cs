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

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(e => e.CustomerId).IsRequired();
            builder.Property(e => e.PhoneNumber).HasMaxLength(10).IsRequired();
            builder.Property(e => e.TotalAmount).IsRequired();
            builder.Property(e => e.OrderDate).IsRequired();

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Orders)
                   .HasForeignKey(x => x.CustomerId);

            builder.HasOne(x => x.Address)
                   .WithMany()
                   .HasForeignKey(x => x.AddressId)
                   .IsRequired();
        }
    }
}
