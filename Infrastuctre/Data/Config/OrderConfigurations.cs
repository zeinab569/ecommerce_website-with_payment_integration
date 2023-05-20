﻿using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastuctre.Data.Config
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {

        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });
            builder.Navigation(a=>a.ShipToAddress).IsRequired();
            builder.Property(a=>a.Status).HasConversion(
                o=>o.ToString(),
                o=>(OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );
            builder.HasMany(o=>o.Items).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
