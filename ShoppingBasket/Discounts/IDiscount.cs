using ShoppingBasket.Checkout;
using ShoppingBasket.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Discounts
{
    public interface IDiscount
    {
        string Code { get; }
        decimal Amount { get; }
        decimal MinimumSpend { get; }
        ICategory Category { get; set; }

        Boolean DiscountIsValidForBasket(IBasket basket, out string errorMessage);
    }
}
