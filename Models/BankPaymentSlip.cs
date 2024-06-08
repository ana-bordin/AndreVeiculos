using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class BankPaymentSlip
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
