using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public interface IProduct
    {
        int productID { get; }
        string ToString();
        decimal AmountPrice();
    }
}
