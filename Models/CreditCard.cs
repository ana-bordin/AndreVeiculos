using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class CreditCard
    {
        [Key]
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
