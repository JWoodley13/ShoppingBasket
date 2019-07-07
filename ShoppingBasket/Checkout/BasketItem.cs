using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Catalog;

namespace ShoppingBasket.Checkout
{
    public class BasketItem : IBasketItem
    {
        public BasketItem(IProduct product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public IProduct Product { get; }
        public int Quantity { get; private set; }
        public decimal Total => Product.Price * Quantity;

        public void IncreaseQuantity(int quantityToAdd)
        {
            Quantity += quantityToAdd;
        }
    }
}
