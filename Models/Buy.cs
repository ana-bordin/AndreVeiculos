using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Buy
    {
        [Key]
        public int Id { get; set; }
        public Car Car { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
