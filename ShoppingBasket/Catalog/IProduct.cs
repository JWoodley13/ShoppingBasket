using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public interface IProduct
    {
        string Name { get; set; }
        decimal Price { get; set; }
        ICategory Category { get; set; }
    }
}
