using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;
using CheckoutRepositories.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary.Validators
{
    public class ItemValidator : IItemValidator
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemValidator(IItemsRepository itemsRepository)
        {
            this._itemsRepository = itemsRepository;
        }

        public Item ValidateItem(string sku)
        {
            var item = _itemsRepository.GetItemBySKU(sku);
            if (item == null)
                throw new UnknownItemException(CheckoutConstants.InvalidItemMessage);

            return item;
        }
    }
}
