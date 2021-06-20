using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;
using CheckoutLibrary.Validators;
using CheckoutRepositories.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace CheckoutLibrary
{
    public class Checkout : ICheckout
    {
        private readonly IItemValidator _itemValidator;

        public Checkout(IItemValidator itemsRepository)
        {
            this._itemValidator = itemsRepository;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            var repositoryItem = _itemValidator.ValidateItem(item);
        }
    }
}
