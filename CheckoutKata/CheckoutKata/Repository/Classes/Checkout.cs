using CheckoutKata.Repository.Interfaces;

namespace CheckoutKata.Repository.Classes
{
    /*
     * Class to implement the checkout interface
     */
    public class Checkout : ICheckout
    {
      
        // Method to the scan the item 
        public void Scan(string item)
        {
            // Validate the item
            if (string.IsNullOrEmpty(item))
            {
                throw new ArgumentException("Error. Please scan an item");
            }
           
        }

        // Method to get the total price of the items scanned
        public int GetTotalPrice()
        {
            return 0;
        }

    }
}
