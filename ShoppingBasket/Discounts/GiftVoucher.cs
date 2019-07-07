using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Catalog;
using ShoppingBasket.Checkout;

namespace ShoppingBasket.Discounts
{
    public class GiftVoucher : IDiscount
    {
        public GiftVoucher(string code, decimal amount)
        {
            Code = code;
            Amount = amount;
        }

        public string Code { get; }
        public decimal Amount { get; }
        public decimal MinimumSpend => 0;
        public ICategory Category { get; set; }

        //not applicable for giftcards, but inherits the same interface incase the functionality needs to be added later.
        public bool DiscountIsValidForBasket(IBasket basket, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }
}
