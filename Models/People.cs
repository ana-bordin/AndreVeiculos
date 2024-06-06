namespace Models
{
    public abstract class People
    {
        public string Document { get; set; } //mascara pk
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Address Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public abstract string Tipo();

        public People(string document, string name, DateOnly birthDate, Address address, string telephone, string email)
        {
            Document = document;
            Name = name;
            BirthDate = birthDate;
            Address = address;
            Telephone = telephone;
            Email = email;
        }
    }
}
