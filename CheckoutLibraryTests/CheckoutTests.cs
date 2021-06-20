using CheckoutLibrary;
using CheckoutRepositories.Items;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System;
using CheckoutLibrary.Constants;
using CheckoutLibrary.Exceptions;

namespace CheckoutLibraryTests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void Scan_When_InvalidItemIsScanned_Then_UnknownItemExceptionIsThrownWithExpectedMessage()
        {
            var itemsRepository = new Mock<IItemsRepository>();
            var checkout = new Checkout(itemsRepository.Object);

            itemsRepository.Setup(_ => _.GetItemBySKU("InvalidItem")).Returns((Item)null).Verifiable();

            Action act = () => checkout.Scan("InvalidItem");

            act.Should().Throw<UnknownItemException>().WithMessage(CheckoutConstants.InvalidItemMessage);
            itemsRepository.Verify();
        }
    }
}
