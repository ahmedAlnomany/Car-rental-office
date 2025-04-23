namespace ahmed7716.Models
{
    public class Rental
    {
        public int RentalID { set; get; }
        public int CustomerID { set; get; }
        public Customer customer { set; get; }
        public int CarID { set; get; }
        public Car car { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public decimal TotalCost { set; get; }
        public string PaymentStatus { set; get; }
    }
}
