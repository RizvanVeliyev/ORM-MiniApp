using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_MiniApp.Configurations
{
    internal sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.Property(o => o.Quantity).IsRequired();
            builder.Property(o => o.PricePerItem).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(o => o.ProductId).IsRequired();
            builder.Property(o => o.OrderId).IsRequired();
            builder.HasCheckConstraint("CK_PricePerItem", "PricePerItem>0");
            builder.HasCheckConstraint("CK_Quantity", "Quantity>0");


        }
    }
}
