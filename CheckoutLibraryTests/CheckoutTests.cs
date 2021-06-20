using CheckoutLibrary;
using CheckoutRepositories.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System;
using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;
using CheckoutLibrary.Validators;

namespace CheckoutLibraryTests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void Scan_When_InvalidItemIsScanned_Then_UnknownItemExceptionIsThrownWithExpectedMessage()
        {
            var itemsRepository = new Mock<IItemsRepository>();
            var itemValidator = new ItemValidator(itemsRepository.Object);
            var checkout = new Checkout(itemValidator);

            itemsRepository.Setup(_ => _.GetItemBySKU("InvalidItem")).Returns((Item)null).Verifiable();

            Action act = () => checkout.Scan("InvalidItem");

            act.Should().Throw<UnknownItemException>().WithMessage(CheckoutConstants.InvalidItemMessage);
            itemsRepository.Verify();
        }

        [TestMethod]
        public void Scan_When_AValidItemIsScanned_Then_ItemIsAddedForCheckout()
        {
            var itemsRepository = new Mock<IItemsRepository>();
            var itemValidator = new ItemValidator(itemsRepository.Object);
            var checkout = new Checkout(itemValidator);
            var item = new Item
            {
                Sku = "ValidItem",
                UnitPrice = 1.0M,
                SpecialPrice = "3 for 2"
            };

            itemsRepository.Setup(_ => _.GetItemBySKU("ValidItem")).Returns(item).Verifiable();

            checkout.Scan("ValidItem");

            checkout.BasketItems.Should().NotBeEmpty();
            checkout.BasketItems[0].Sku.Should().Be(item.Sku);
            checkout.BasketItems[0].UnitPrice.Should().Be(item.UnitPrice);
            checkout.BasketItems[0].SpecialPrice.Should().Be(item.SpecialPrice);
            checkout.BasketItems[0].Qty.Should().Be(1);
            itemsRepository.Verify();
        }

        [TestMethod]
        public void Scan_When_AValidItemIsScannedTwice_Then_ItemQuantityIsUpdatedForCheckout()
        {
            var itemsRepository = new Mock<IItemsRepository>();
            var itemValidator = new ItemValidator(itemsRepository.Object);
            var checkout = new Checkout(itemValidator);
            var item = new Item
            {
                Sku = "ValidItem",
                UnitPrice = 1.0M,
                SpecialPrice = "3 for 2"
            };

            var basketItem = new BasketItem
            {
                Sku = "ValidItem",
                UnitPrice = 1.0M,
                Qty = 1,
                SpecialPrice = "3 for 2"
            };

            checkout.BasketItems.Add(basketItem);

            itemsRepository.Setup(_ => _.GetItemBySKU("ValidItem")).Returns(item).Verifiable();

            checkout.Scan("ValidItem");

            checkout.BasketItems.Should().NotBeEmpty();
            checkout.BasketItems[0].Sku.Should().Be(item.Sku);
            checkout.BasketItems[0].UnitPrice.Should().Be(item.UnitPrice);
            checkout.BasketItems[0].SpecialPrice.Should().Be(item.SpecialPrice);
            checkout.BasketItems[0].Qty.Should().Be(2);
            itemsRepository.Verify();
        }
    }
}
