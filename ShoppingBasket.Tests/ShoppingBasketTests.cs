using NUnit.Framework;
using ShoppingBasket.Checkout;
using ShoppingBasket.Catalog;
using ShoppingBasket.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Tests
{
    [TestFixture]
    public class ShoppingBasketTests
    {
        #region Basket1 Test

        //Basket 1:
        //1 Hat @ £10.50
        //1 Jumper @ £54.65
        //------------
        //1 x £5.00 Gift Voucher XXX-XXX applied
        //------------
        //Total: £60.15

        [Test]
        public void Basket1()
        {
            IProduct hat = new Product("hat", new decimal(10.50));
            IProduct jumper = new Product("jumper", new decimal(54.65));

            IDiscount giftCard = new GiftVoucher("XXX-XXX", new decimal(5.00));

            IBasket basket = new Basket();
            basket.AddProduct(hat, 1);
            basket.AddProduct(jumper, 1);
            basket.AddDiscount(giftCard);

            Assert.AreEqual(60.15, basket.Total);
        }

        #endregion

        #region Basket2 Test

        //Basket 2:
        //1 Hat @ £25.00
        //1 Jumper @ £26.00
        //------------
        //1 x £5.00 off Head Gear in baskets over £50.00 Offer Voucher YYY-YYY applied
        //------------
        //Total: £51.00
        //Message: “There are no products in your basket applicable to voucher Voucher YYY-YYY.”

        [Test]
        public void Basket2()
        {
            IProduct hat = new Product("hat", new decimal(25.00));
            IProduct jumper = new Product("jumper", new decimal(26.00));

            ICategory headGearCategory = new Category("headgear");

            IDiscount headgearOffer = new OfferVoucher("YYY-YYY", new decimal(5.00), new decimal(50.00), headGearCategory);

            IBasket basket = new Basket();
            basket.AddProduct(hat, 1);
            basket.AddProduct(jumper, 1);

            basket.AddDiscount(headgearOffer);

            Assert.AreEqual(true, basket.ErrorInBasket);
            Assert.IsNotEmpty(basket.ErrorMessage);
        }


        #endregion

        #region Basket3 Test

        //Basket 3:
        //1 Hat @ £25.00
        //1 Jumper @ £26.00
        //1 Head Light(Head Gear Category of Product) @ £3.50
        //------------
        //1 x £5.00 off Head Gear in baskets over £50.00 Offer Voucher YYY-YYY applied
        //------------

        //Total: £51.00

        [Test]
        public void Basket3()
        {
            ICategory headGearCategory = new Category("headgear");

            IProduct hat = new Product("hat", new decimal(25.00));
            IProduct jumper = new Product("jumper", new decimal(26.00));
            IProduct headLight = new Product(headGearCategory, "head light", new decimal(3.50));

            IDiscount headgearOffer = new OfferVoucher("YYY-YYY", new decimal(5.00), new decimal(50.00), headGearCategory);

            IBasket basket = new Basket();
            basket.AddProduct(hat, 1);
            basket.AddProduct(jumper, 1);
            basket.AddProduct(headLight, 1);

            basket.AddDiscount(headgearOffer);

            Assert.AreEqual(51.00, basket.Total);
        }

        #endregion

        #region Basket4 Test

        //Basket 4:
        //1 Hat @ £25.00
        //1 Jumper @ £26.00
        //------------
        //1 x £5.00 Gift Voucher XXX-XXX applied
        //1 x £5.00 off baskets over £50.00 Offer Voucher YYY-YYY applied
        //------------
        //Total: £41.00

        [Test]
        public void Basket4()
        {
            IProduct hat = new Product("hat", new decimal(25.00));
            IProduct jumper = new Product("jumper", new decimal(26.00));

            IDiscount giftCard = new GiftVoucher("XXX-XXX", new decimal(5.00));
            IDiscount offerVoucher = new OfferVoucher("YYY-YYY", new decimal(5.00), new decimal(50.00));

            IBasket basket = new Basket();
            basket.AddProduct(hat, 1);
            basket.AddProduct(jumper, 1);

            basket.AddDiscount(giftCard);
            basket.AddDiscount(offerVoucher);

            Assert.AreEqual(41.00, basket.Total);
        }

        #endregion

        #region Basket5 Test

        //Basket 5:
        //1 Hat @ £25.00
        //1 £30 Gift Voucher @ £30.00
        //------------
        //1 x £5.00 off baskets over £50.00 Offer Voucher YYY-YYY applied
        //------------
        //Total: £55.00
        //------------
        //Message: “You have not reached the spend threshold for voucher YYY-YYY.Spend another £25.01 to
        //receive £5.00 discount from your basket total.”

        [Test]
        public void Basket5()
        {
            IProduct hat = new Product("hat", new decimal(25.00));
            IProduct giftCard = new GiftCard("£30 Gift Voucher", new decimal(30.00));

            IDiscount offerVoucher = new OfferVoucher("YYY-YYY", new decimal(5.00), new decimal(50.00));

            IBasket basket = new Basket();
            basket.AddProduct(hat, 1);
            basket.AddProduct(giftCard, 1);

            basket.AddDiscount(offerVoucher);

            Assert.AreEqual(true, basket.ErrorInBasket);
            Assert.IsNotEmpty(basket.ErrorMessage);
        }

        #endregion

        #region Basket6 Test

        //Basket 6:
        //1 Jumper @ £54.65
        //1 Head Light(Head Gear Category of Product) @ £3.50
        //------------
        //No vouchers applied
        //------------
        //Total: £58.15
        //------------

        [Test]
        public void Basket6()
        {
            ICategory headGearCategory = new Category("headgear");

            IProduct jumper = new Product("jumper", new decimal(54.65));
            IProduct headLight = new Product(headGearCategory, "head light", new decimal(3.50));

            IBasket basket = new Basket();
            basket.AddProduct(jumper, 1);
            basket.AddProduct(headLight, 1);

            Assert.AreEqual(58.15, basket.Total);

        }

        #endregion
    }
}
