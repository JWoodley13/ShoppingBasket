using ShoppingBasket.Checkout;
using ShoppingBasket.Discounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public class GiftCard : IProduct
    {
        public GiftCard(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICategory Category { get; set; }

    }
}
