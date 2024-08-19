using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(p => p.Value, dbId => OrderId.Of(dbId));
            //builder.Property(x => x.OrderName).HasConversion(p => p.Value, dbId => OrderName.Of(dbId));
            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId);
            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(y => y.OrderId);

            builder.ComplexProperty(x => x.OrderName, nameBuilder =>
                { nameBuilder.Property(p => p.Value).HasColumnName(nameof(OrderName)).HasMaxLength(200).IsRequired(); });

            builder.ComplexProperty(x => x.BillingAddress,
                addressBuilder =>
                {
                    addressBuilder.Property(p => p.City).HasMaxLength(100).IsRequired();
                    addressBuilder.Property(p => p.Country).HasMaxLength(100).IsRequired();
                    addressBuilder.Property(p => p.Email).HasMaxLength(255);
                    addressBuilder.Property(p => p.FirstName).HasMaxLength(100);
                    addressBuilder.Property(p => p.LastName).HasMaxLength(100);
                    addressBuilder.Property(p => p.Street).HasMaxLength(500).IsRequired();
                });

            builder.ComplexProperty(x => x.ShippingAddress,
                addressBuilder =>
                {
                    addressBuilder.Property(p => p.City).HasMaxLength(100).IsRequired();
                    addressBuilder.Property(p => p.Country).HasMaxLength(100).IsRequired();
                    addressBuilder.Property(p => p.Email).HasMaxLength(255);
                    addressBuilder.Property(p => p.FirstName).HasMaxLength(100);
                    addressBuilder.Property(p => p.LastName).HasMaxLength(100);
                    addressBuilder.Property(p => p.Street).HasMaxLength(500).IsRequired();
                });

            //builder.ComplexProperty(x => x.Payment, paymentBuilder =>
            //{
            //    paymentBuilder.Property(p => p.CardName).HasMaxLength(100);
            //    paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
            //    paymentBuilder.Property(p => p.Expiration).HasMaxLength(20).IsRequired();
            //    paymentBuilder.Property(p => p.Cvv).HasMaxLength(3).IsRequired();
            //    paymentBuilder.Property(p => p.PaymentMethod);
            //});

            builder.OwnsOne(x => x.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName).HasMaxLength(100);
                paymentBuilder.Property(p => p.CardNumber).HasMaxLength(24).IsRequired();
                paymentBuilder.Property(p => p.Expiration).HasMaxLength(20).IsRequired();
                paymentBuilder.Property(p => p.Cvv).HasMaxLength(3).IsRequired();
                paymentBuilder.Property(p => p.PaymentMethod);
                paymentBuilder.ToTable(paymentBuilder.OwnedEntityType.ClrType.Name);
            });

            builder.Property(x => x.Status)
                .HasDefaultValue(OrderStatus.Submitted)
                .HasMaxLength(15)
                .HasConversion(x => x.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
            //.HasConversion(x => (int)x, dbStatus => (OrderStatus)dbStatus);
            builder.Property(x => x.TotalPrice).IsRequired();

        }
    }
}
