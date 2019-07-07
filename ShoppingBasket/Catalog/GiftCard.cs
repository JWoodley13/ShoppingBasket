using ShoppingBasket.Checkout;
using ShoppingBasket.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public class GiftCard : IProduct, IDiscount
    {
        public GiftCard(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICategory Category { get; set; }

        public string Code { get; }
        public decimal Amount { get; }
        public decimal MinimumSpend => 0;

        public bool DiscountIsValidForBasket(IBasket basket, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }
}
