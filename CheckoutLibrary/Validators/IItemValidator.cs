using CheckoutRepositories.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary.Validators
{
    public interface IItemValidator
    {
        Item ValidateItem(string sku);
    }
}
