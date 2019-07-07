using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Checkout;
using ShoppingBasket.Catalog;

namespace ShoppingBasket.Discounts
{
    public class OfferVoucher : Discount
    {
        public OfferVoucher(string code, decimal amount, decimal minimumSpend) : base(code, amount, minimumSpend)
        {
        }

        public OfferVoucher(string code, decimal amount, decimal minimumSpend, ICategory category) : this(code, amount, minimumSpend)
        {
            Category = category;
        }

        public override bool DiscountIsValidForBasket(IBasket basket, out string errorMessage)
        {
            errorMessage = string.Empty;

            //if the voucher is applied to a certain category, return the list of products in the category, else return all of the products in the basket.
            IList<IBasketItem> productsInCategory = Category != null ? basket.Products.Where(x => x.Product.Category == Category).ToList() : basket.Products.Where(x => x.Product.GetType() == typeof(Product)).ToList();

            if (productsInCategory.Count == 0)
            {
                errorMessage = $"There are no products in your basket applicable to voucher {Code}.";
                return false;
            }

            if (basket.DiscountableTotal < MinimumSpend)
            {
                errorMessage = $"You have not reached the spend threshold for voucher {Code}. Spend another {(MinimumSpend - basket.DiscountableTotal).ToString("F")} to receive {Amount} discount from your basket total.";
                return false;
            }

            return true;
        }
    }
}
