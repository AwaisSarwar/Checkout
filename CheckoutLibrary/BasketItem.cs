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

        public decimal GetPrice()
        {
            if (string.IsNullOrWhiteSpace(this.SpecialPrice))
                return this.UnitPrice * this.Qty;
            else
            {
                return GetSpecialPriceTotal();
            }
        }

        public decimal GetSpecialPriceTotal()
        {
            var specialPriceBreakdown = this.SpecialPrice.Split(" for ");
            var specialPriceQty = Convert.ToInt32(specialPriceBreakdown[0]);
            var specialPricePrice = Convert.ToDecimal(specialPriceBreakdown[1]);

            if (this.Qty < specialPriceQty)
                return this.UnitPrice * this.Qty;
            else
            {
                return specialPricePrice * (this.Qty / specialPriceQty) +
                    (this.Qty % specialPriceQty) * this.UnitPrice;
            }
        }
    }
}
