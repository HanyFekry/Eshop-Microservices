﻿namespace CatalogApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public List<string> Category { get; set; } = default!;
        public double Price { get; set; }
        public string ImageUrl { get; set; } = default!;

    }
}
