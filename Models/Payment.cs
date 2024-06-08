using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        public CreditCard CreditCard { get; set; }
        public BankPaymentSlip BankPaymentSlip { get; set; }
        public Pix Pix { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
