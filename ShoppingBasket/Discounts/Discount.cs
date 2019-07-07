using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Catalog;
using ShoppingBasket.Checkout;

namespace ShoppingBasket.Discounts
{
    public abstract class Discount : IDiscount
    {
        public Discount(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public Discount(string code, decimal amount, decimal minimumSpend) : this(code, amount)
        {
            MinimumSpend = minimumSpend;
        }

        public string Code { get; }

        public decimal Amount { get; }

        public decimal MinimumSpend { get; }

        public ICategory Category { get; set; }

        public abstract bool DiscountIsValidForBasket(IBasket basket, out string errorMessage);
    }
}
