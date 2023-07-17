using CheckoutKata.Repository.Classes;
using CheckoutKata.Repository.Interfaces;
using NUnit.Framework;

namespace CheckoutKata.Tests
{
    /*
     * Tests for Checkout
     * Red - Green - Refractor
     */
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void Scan_EmptyItemScan_ReturnException()
        {
            // Arrange
            ICheckout checkout = new Checkout();

            // Assert
            Assert.Throws<ArgumentException>(() => checkout.Scan(""));
        }
    }
}
