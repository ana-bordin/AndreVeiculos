namespace Models
{
    public class Buy
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
