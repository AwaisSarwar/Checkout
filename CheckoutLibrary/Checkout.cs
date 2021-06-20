using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;
using CheckoutRepositories.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary
{
    public class Checkout : ICheckout
    {
        private readonly IItemsRepository _itemsRepository;

        public Checkout(IItemsRepository itemsRepository)
        {
            this._itemsRepository = itemsRepository;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            var repositoryItem = _itemsRepository.GetItemBySKU(item);
            if (repositoryItem == null)
                throw new UnknownItemException(CheckoutConstants.InvalidItemMessage);
        }
    }
}
