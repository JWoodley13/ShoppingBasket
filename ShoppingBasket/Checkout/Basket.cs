using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Discounts;
using ShoppingBasket.Catalog;

namespace ShoppingBasket.Checkout
{
    public class Basket : IBasket
    {
        public Basket()
        {
            Products = new List<IBasketItem>();
            Discounts = new List<IDiscount>();

            ProductAddedToCart += RecalculateTotals;
            DiscountAddedToCart += RecalculateTotals;
        }

        public IList<IBasketItem> Products { get; }
        public IList<IDiscount> Discounts { get; }
        public decimal SubTotal { get; private set; }
        public decimal DiscountTotal { get; private set; }
        //Calculate the basket total without any GiftCard's.
        //As GiftCards are stored as a different type, totalling all products will return the correct amount.
        public decimal DiscountableTotal => Products.Where(x => x.Product.GetType() == typeof(Product)).Sum(x => x.Total);
        public decimal Total { get; private set; }
        public bool ErrorInBasket { get; private set; }
        public string ErrorMessage { get; private set; }

        public event EventHandler ProductAddedToCart;
        public event EventHandler DiscountAddedToCart;

        public void AddDiscount(IDiscount discount)
        {
            if(discount.GetType() == typeof(OfferVoucher))
            {
                AddOfferVoucher((OfferVoucher)discount);
            }

            if(!ErrorInBasket)
            {
                Discounts.Add(discount);
                OnDiscountAdded(EventArgs.Empty);
            }
        }

        private void AddOfferVoucher(OfferVoucher voucher)
        {
            if (Discounts.Count(x => x.GetType() == typeof(OfferVoucher)) > 0)
            {
                ErrorInBasket = true;
                ErrorMessage = "You can only have one offer voucher active at one time";
                return;
            }

            string errorMessage = string.Empty;
            if (!voucher.DiscountIsValidForBasket(this, out errorMessage))
            {
                ErrorInBasket = true;
                ErrorMessage = errorMessage;
                return;
            }
        }

        private void OnDiscountAdded(EventArgs e)
        {
            DiscountAddedToCart?.Invoke(this, e);
        }

        public void AddProduct(IProduct product, int quantity)
        {
            if (Products.Count(x => x.Product == product) > 0)
                Products.Single(x => x.Product == product).IncreaseQuantity(quantity);
            else
                Products.Add(new BasketItem(product, quantity));

            OnProductAdded(EventArgs.Empty);
        }

        private void OnProductAdded(EventArgs e)
        {
            ProductAddedToCart?.Invoke(this, e);
        }

        private void RecalculateTotals(object sender, EventArgs e)
        {
            SubTotal = Products.Sum(x => x.Total);
            CalculateDiscount();
            Total = SubTotal - DiscountTotal;
        }

        private void CalculateDiscount()
        {
            //total discount from giftcards
            decimal giftCardDiscount = Discounts.Where(x => x.GetType() == typeof(GiftVoucher)).Sum(x => x.Amount);

            //we know we can only have one offer voucher, so SingleOrDefault will either return the voucher, or null
            OfferVoucher voucher = (OfferVoucher)Discounts.SingleOrDefault(x => x.GetType() == typeof(OfferVoucher));

            if(voucher == null)
            {
                DiscountTotal = giftCardDiscount;
                return;
            }

            decimal voucherDiscount = 0;

            //if the voucher is applied to a certain category, return the sum of products in the category, else return the sum of all the products in the basket.
            decimal voucherCategoryProductTotal = voucher.Category != null ? Products.Where(x => x.Product.Category == voucher.Category).Sum(x => x.Total) : SubTotal;

            voucherDiscount = voucherCategoryProductTotal > voucher.Amount ? voucher.Amount : voucherCategoryProductTotal;

            //if the discountable total is less than the discounts added, then the max discount is used - this is because giftcards aren't available for discount.
            DiscountTotal = DiscountableTotal < giftCardDiscount + voucherDiscount ? DiscountableTotal : giftCardDiscount + voucherDiscount;
        }
    }
}
