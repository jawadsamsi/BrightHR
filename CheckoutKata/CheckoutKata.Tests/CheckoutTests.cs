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

        [Test]
        public void GetTotalPrice_NoItemScan_ReturnZero()
        {
            // Arrange
            int expected = 0;
            ICheckout checkout = new Checkout();

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTotalPrice_OneItemScan_ReturnItemPrice()
        {
            // Arrange
            ICheckout checkout = new Checkout();
            checkout.Scan("A");

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(50, actual);
        }
    }
}
