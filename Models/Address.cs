using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Address
    {
        public int Id { get; set; }
        public string TypeStreet { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }   
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
       
        public override string ToString()
        {
            return $"Street: {TypeStreet} {Street}, Number: {Number}, Complement: {Complement}, Neighborhood: {Neighborhood}, City: {City}, State: {State}, ZipCode: {ZipCode}";
        }
    }
}
