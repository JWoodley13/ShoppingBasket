using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public class Product : IProduct
    {
        public Product() { }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Product(ICategory category, string name, decimal price) : this(name, price)
        {
            Category = category;
        }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICategory Category { get; set; }
    }
}
