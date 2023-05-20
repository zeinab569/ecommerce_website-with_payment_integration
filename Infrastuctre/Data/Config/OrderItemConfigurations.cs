﻿using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastuctre.Data.Config
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {

        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(i => i.ItemOrderd, io =>
            {
                io.WithOwner();
            });
            builder.Property(i => i.Price).HasColumnType("decimal(18,2)");
        }
    }
}
