using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public class Category : ICategory
    {
        public Category()
        {
            Products = new List<IProduct>();
        }

        public Category(string name) : this()
        {
            Name = name;
        }

        public string Name { get; set; }
        public IList<IProduct> Products { get; set; }
    }
}
