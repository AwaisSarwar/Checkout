using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary
{
    public class BasketItem
    {
        public string Sku { get; set; }
        public decimal UnitPrice { get; set; }
        public int Qty { get; set; }
        public string SpecialPrice { get; set; }
    }
}
