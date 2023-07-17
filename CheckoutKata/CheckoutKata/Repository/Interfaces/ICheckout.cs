namespace CheckoutKata.Repository.Interfaces
{
    /*
     * Interface to carry out the checkout operations
     */
    public interface ICheckout
    {
        void Scan(string item);
    }
}
