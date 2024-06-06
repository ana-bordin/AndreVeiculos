namespace Models
{
    public class Payment
    {
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public BankPaymentSlip BankPaymentSlip { get; set; }
        public Pix Pix { get; set; }
        public DateOnly PaymentDate { get; set; }
    }
}
