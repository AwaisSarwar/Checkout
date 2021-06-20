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
        private IList<BasketItem> _basketItems;

        public IList<BasketItem> BasketItems => _basketItems;

        public Checkout(IItemValidator itemsRepository)
        {
            this._basketItems = new List<BasketItem>();
            this._itemValidator = itemsRepository;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            var repositoryItem = _itemValidator.ValidateItem(item);
            _basketItems.Add(new BasketItem
            {
                Sku = repositoryItem.Sku,
                Qty = 1,
                UnitPrice = repositoryItem.UnitPrice,
                SpecialPrice = repositoryItem.SpecialPrice
            }) ;
        }
    }
}
