using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;
using CheckoutLibrary.Validators;
using CheckoutRepositories.Items;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public decimal GetTotalPrice()
        {
            return _basketItems.Sum(_ => _.GetPrice());
        }

        public void Scan(string item)
        {
            var repositoryItem = _itemValidator.ValidateItem(item);
            AddItemToBasket(item, repositoryItem);
        }

        private void AddItemToBasket(string item, Item repositoryItem)
        {
            var basketItem = _basketItems.FirstOrDefault(_ => _.Sku == item) ?? new BasketItem
            {
                Sku = repositoryItem.Sku,
                UnitPrice = repositoryItem.UnitPrice,
                SpecialPrice = repositoryItem.SpecialPrice
            };

            basketItem.Qty++;

            if(basketItem.Qty == 1)
                _basketItems.Add(basketItem);
        }
    }
}
