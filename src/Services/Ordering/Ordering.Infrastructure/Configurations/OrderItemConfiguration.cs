
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasConversion(p => p.Value, dbId => OrderItemId.Of(dbId));

            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            //builder.HasOne<Order>()
            //    .WithMany(y => y.OrderItems)
            //    .HasForeignKey(x => x.OrderId);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId);

        }
    }
}
