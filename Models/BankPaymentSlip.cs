namespace Models
{
    public class BankPaymentSlip
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}
