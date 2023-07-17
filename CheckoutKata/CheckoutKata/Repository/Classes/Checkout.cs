using CheckoutKata.Models;
using CheckoutKata.Repository.Interfaces;

namespace CheckoutKata.Repository.Classes
{
    /*
     * Class to implement the checkout interface
     */
    public class Checkout : ICheckout
    {
        // Initialise the parameters
        private readonly List<SKUPriceModel> _skuPriceList;
        private readonly Dictionary<string, int> _scannedItems;

        // Constructor
        public Checkout(List<SKUPriceModel> skuPriceList)
        {
            _skuPriceList = skuPriceList;
            _scannedItems = new Dictionary<string, int>();            
        }

        // Method to the scan the item 
        public void Scan(string item)
        {
            // Validate the item
            if (string.IsNullOrEmpty(item))
            {
                throw new ArgumentException("Error. Please scan an item");
            }

            // Check if the item exists in the sku list
            var itemData = _skuPriceList.FirstOrDefault(s => s.Name == item); 
            if (itemData == null)
            {
                throw new ArgumentException("Error. Invalid item");
            }

            // If exists
            if (_scannedItems.Keys.Contains(item))
            {
                _scannedItems[item]++;
            }
            else
            {
                _scannedItems.Add(item, 1);
            }
        }

        // Method to get the total price of the items scanned
        public int GetTotalPrice()
        {
            // Parameters
            int totalPrice = 0;

            // Loop each scanned item and return the total list
            foreach (var item in _scannedItems)
            {
                // Get the individual item data
                var itemData = _skuPriceList.FirstOrDefault(s => s.Name == item.Key);

                // For the special price
                if (itemData != null && itemData.SKUSpecialPrice != null)
                {
                    // Get the special quantity and the remaing quantity and then total it
                    int specialQuantity = item.Value / itemData.SKUSpecialPrice.Quantity;
                    int remainingQuantity = item.Value % itemData.SKUSpecialPrice.Quantity;
                    totalPrice = totalPrice + (specialQuantity * itemData.SKUSpecialPrice.SpecialPrice) + (remainingQuantity * itemData.Price);
                }
                // For the individual price
                else if(itemData != null)
                {
                    totalPrice = totalPrice + itemData.Price;
                }
            }
            return totalPrice;
        }

    }
}
