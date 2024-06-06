namespace Models
{
    public class CreditCard
    {
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string SecurityCode { get; set; }
        public DateOnly ExpirationDate { get; set; }
    }
}
