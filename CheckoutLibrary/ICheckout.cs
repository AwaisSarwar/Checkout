using System;

namespace CheckoutLibrary
{
    public interface ICheckout
    {
        void Scan(string item);
        int GetTotalPrice();
    }
}
