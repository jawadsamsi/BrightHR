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
        // Method to instantiate the checkout
        private static Checkout CreateCheckout()
        {
            var skuPriceList = new List<SKUPriceModel>()
            {
                new SKUPriceModel{Name= "A", Price = 50, SKUSpecialPrice = new SKUSpecialPriceModel(){ Quantity = 3, SpecialPrice = 130 }},
                new SKUPriceModel{Name= "B", Price = 30, SKUSpecialPrice = new SKUSpecialPriceModel(){ Quantity = 2, SpecialPrice = 45 }},
                new SKUPriceModel{Name= "C", Price = 20},
                new SKUPriceModel{Name= "D", Price = 15}
            };
            return new Checkout(skuPriceList);
        }

        [TestFixture]
        public class Scan
        {
            [Test]
            public void EmptyItemScan_ReturnException()
            {
                // Arrange
                ICheckout checkout = CreateCheckout();

                // Assert
                Assert.Throws<ArgumentException>(() => checkout.Scan(""));
            }

            [Test]
            public void InvalidItem_ReturnException()
            {
                // Arrange
                ICheckout checkout = CreateCheckout();

                // Assert
                Assert.Throws<ArgumentException>(() => checkout.Scan("E"));
            }
        }

        [TestFixture]
        public class GetTotalPrice
        {
            [Test]
            public void NoItemScan_ReturnZero()
            {
                // Arrange
                int expected = 0;
                ICheckout checkout = CreateCheckout();

                // Act
                int actual = checkout.GetTotalPrice();

                // Assert
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void OneItemScan_ReturnItemPrice()
            {
                // Arrange
                int expected = 50;
                ICheckout checkout = CreateCheckout();
                checkout.Scan("A");

                // Act
                int actual = checkout.GetTotalPrice();

                // Assert
                Assert.AreEqual(expected, actual);
            }



            [Test]
            public void MultipleItemScan_ReturnItemPrices()
            {
                // Arrange
                int expected = 115;
                ICheckout checkout = CreateCheckout();
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
            public void SpecialPriceItemScan_ReturnSpecialPrice()
            {
                // Arrange
                int expected = 130;
                ICheckout checkout = CreateCheckout();
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");

                // Act
                int actual = checkout.GetTotalPrice();

                // Assert
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void MultipleSpecialPriceItemScan_ReturnSpecialPrice()
            {
                // Arrange
                int expected = 175;
                ICheckout checkout = CreateCheckout();
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

            [Test]
            public void OneItemAlongWithSpecialPriceItemScan_ReturnTotalPrice()
            {
                // Arrange
                int expected = 180;
                ICheckout checkout = CreateCheckout();
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");

                // Act
                int actual = checkout.GetTotalPrice();

                // Assert
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void MultipleItemsAlongWithSpecialPricesItemScan_ReturnTotalPrice()
            {
                // Arrange
                int expected = 210;
                ICheckout checkout = CreateCheckout();
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("B");
                checkout.Scan("B");
                checkout.Scan("C");
                checkout.Scan("D");

                // Act
                int actual = checkout.GetTotalPrice();

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }

        

    }
}
