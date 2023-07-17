using CheckoutKata.Models;
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
        // Initialise the parameters
        private List<SKUPriceModel> skuPriceList;

        [SetUp]
        public void Setup()
        {
            skuPriceList = new List<SKUPriceModel>()
            {
                new SKUPriceModel{Name= "A", Price = 50, SKUSpecialPrice = new SKUSpecialPriceModel(){ Quantity = 3, SpecialPrice = 130 }},
                new SKUPriceModel{Name= "B", Price = 30, SKUSpecialPrice = new SKUSpecialPriceModel(){ Quantity = 2, SpecialPrice = 45 }},
                new SKUPriceModel{Name= "C", Price = 20},
                new SKUPriceModel{Name= "D", Price = 15}
            };
        }

        [Test]
        public void Scan_EmptyItemScan_ReturnException()
        {
            // Arrange
            ICheckout checkout = new Checkout(skuPriceList);

            // Assert
            Assert.Throws<ArgumentException>(() => checkout.Scan(""));
        }

        [Test]
        public void GetTotalPrice_NoItemScan_ReturnZero()
        {
            // Arrange
            int expected = 0;
            ICheckout checkout = new Checkout(skuPriceList);

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTotalPrice_OneItemScan_ReturnItemPrice()
        {
            // Arrange
            int expected = 50;
            ICheckout checkout = new Checkout(skuPriceList);
            checkout.Scan("A");

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Scan_InvalidItem_ReturnException()
        {
            // Arrange
            ICheckout checkout = new Checkout(skuPriceList);

            // Assert
            Assert.Throws<ArgumentException>(() => checkout.Scan("E"));
        }

        [Test]
        public void GetTotalPrice_MultipleItemScan_ReturnItemPrices()
        {
            // Arrange
            int expected = 115;
            ICheckout checkout = new Checkout(skuPriceList);
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTotalPrice_SpecialPriceItemScan_ReturnSpecialPrice()
        {
            // Arrange
            int expected = 130;
            ICheckout checkout = new Checkout(skuPriceList);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTotalPrice_MultipleSpecialPriceItemScan_ReturnSpecialPrice()
        {
            // Arrange
            int expected = 175;
            ICheckout checkout = new Checkout(skuPriceList);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("B");

            // Act
            int actual = checkout.GetTotalPrice();

            // Assert
            Assert.AreEqual(expected, actual);
        }

    }
}
