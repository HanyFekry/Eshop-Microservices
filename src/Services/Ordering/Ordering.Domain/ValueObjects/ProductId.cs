﻿
namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; private set; }
        public ProductId(Guid value) => Value = value;

        public static ProductId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
                throw new DomainException("Product Id can not be empty.!");
            return new ProductId(value);
        }
    }
}
