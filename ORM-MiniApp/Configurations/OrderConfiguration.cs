﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ORM_MiniApp.Models;

namespace ORM_MiniApp.Configurations
{
    internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.UserId).IsRequired();
            builder.Property(o=>o.TotalAmount).IsRequired().HasColumnType("decimal(6,2)");
            builder.Property(o => o.Status).IsRequired();
            builder.HasCheckConstraint("CK_TotalAmount","TotalAmount>0");
        }
    }
}
