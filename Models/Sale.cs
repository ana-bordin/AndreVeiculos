namespace Models
{
    public class Sale
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public DateOnly SaleDate { get; set; }
        public Decimal Value { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public Payment Payment { get; set; }
    }
}
