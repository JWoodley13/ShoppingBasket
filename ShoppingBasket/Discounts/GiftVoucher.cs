using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Catalog;
using ShoppingBasket.Checkout;

namespace ShoppingBasket.Discounts
{
    public class GiftVoucher : Discount
    {
        public GiftVoucher(string code, decimal amount) : base(code, amount)
        {
        }

        //not applicable for giftcards, but inherits the same interface incase the functionality needs to be added later.
        public override bool DiscountIsValidForBasket(IBasket basket, out string errorMessage)
        {
            errorMessage = string.Empty;
            return true;
        }
    }
}
