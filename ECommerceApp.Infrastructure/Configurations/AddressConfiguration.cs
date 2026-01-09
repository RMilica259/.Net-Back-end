using ECommerceApp.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Infrastructure.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Id).UseIdentityColumn();

            builder.Property(x => x.City).IsRequired();
            builder.Property(x => x.Street).IsRequired();
            builder.Property(x => x.HouseNumber).IsRequired();
            builder.Property(x => x.ZipCode).IsRequired();
        }
    }
}
