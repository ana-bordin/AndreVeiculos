using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public Car Car { get; set; }
        public DateTime SaleDate { get; set; }
        public Decimal Value { get; set; }
        public Client Client { get; set; }
        public Employee Employee { get; set; }
        public Payment Payment { get; set; }
    }
}
