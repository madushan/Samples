using System;
using System.Collections.Generic;
using System.Text;

namespace Singleton
{
    public class Product
    {
        private static readonly Product product = new Product();
        private Product() { }
        public static Product GetProduct
        {
            get
            {
                return product;
            }
        }
    }
}
