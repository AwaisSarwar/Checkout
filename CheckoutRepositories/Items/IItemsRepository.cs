using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutRepositories.Items
{
    public interface IItemsRepository
    {
        Item GetItemBySKU(string sku);
    }
}
