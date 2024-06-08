using System.ComponentModel.DataAnnotations;

namespace Models
{
    public abstract class Person
    {
        [Key]
        public string Document { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        //public abstract string Tipo();

        //public Person(string document, string name, DateOnly birthDate, Address address, string telephone, string email)
        //{
        //    Document = document;
        //    Name = name;
        //    BirthDate = birthDate;
        //    Address = address;
        //    Telephone = telephone;
        //    Email = email;
        //}
    }
}
