﻿
using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Customer> Customers { get; }
        public DbSet<Product> Products { get; }
        public DbSet<OrderItem> OrderItems { get; }
        public DbSet<Order> Orders { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
