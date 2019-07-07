using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBasket.Catalog
{
    public interface ICategory
    {
        string Name { get; set; }
        IList<IProduct> Products { get; set; }
    }
}
