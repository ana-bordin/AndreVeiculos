namespace Models
{
    public class Employee : People
    {
        public PositionCompany PositionCompany { get; set; }
        public Decimal CommissionPercentage { get; set; }
        public Decimal Commission { get; set; }
        public override string Tipo()
        {
            return "Employee";
        }
        public Employee(string document, string name, DateOnly birthDate, Address address, string telephone, string email, PositionCompany positionCompany, Decimal commissionPercentage, Decimal commission) : base(document, name, birthDate, address, telephone, email)
        {
            PositionCompany = positionCompany;
            CommissionPercentage = commissionPercentage;
            Commission = commission;
        }
    }
}
