using ShoppingBasket.Discounts;
using ShoppingBasket.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Checkout
{
    public interface IBasket
    {
        IList<IBasketItem> Products { get; }
        IList<IDiscount> Discounts { get; }
        decimal SubTotal { get; }
        decimal DiscountTotal { get; }
        decimal Total { get; }
        decimal DiscountableTotal { get; }
        bool ErrorInBasket { get; }
        string ErrorMessage { get; }

        void AddProduct(IProduct product, int quantity);
        void AddDiscount(IDiscount discount);

        event EventHandler ProductAddedToCart;
        event EventHandler DiscountAddedToCart;
    }
}
