using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class People
    {
        public string Document { get; set; } //mascara pk
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public Address Adress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }    
    }
}
