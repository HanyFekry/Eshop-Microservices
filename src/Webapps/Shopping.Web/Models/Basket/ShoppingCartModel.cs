﻿namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItemModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);
    }
    public class ShoppingCartItemModel
    {
        public Guid ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; } = default!;
    }
    public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCartModel Cart);

    public record StoreBasketRequest(ShoppingCartModel Cart);
    public record StoreBasketResponse(string UserName);

    public record DeleteBasketResponse(bool Success);
}
