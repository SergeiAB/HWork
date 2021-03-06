using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryLibrary
{
    public class IDGenerator
    {
        public static int GetID(List<Product> products)
        {
            int i = 0;
            int id = 1;
            if (products != null && products.Count > 0)
            {
                int[] arrayID = new int[products.Count];
                foreach (Product prd in products)
                {
                    arrayID[i] = prd.productID;
                    i++;
                }
                id = arrayID.Max() + 1;
            }
            return id;

        }
    }
}
