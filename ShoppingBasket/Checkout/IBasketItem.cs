using ShoppingBasket.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Checkout
{
    public interface IBasketItem
    {
        IProduct Product { get; }
        int Quantity { get; }
        decimal Total { get; }

        void IncreaseQuantity(int quantityToAdd);
    }
}
