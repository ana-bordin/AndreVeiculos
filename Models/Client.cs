using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Client : People
    {
        
        public decimal PersonIncome { get; set; }
        public string PDF { get; set; }
        public override string Tipo()
        {
            return "Client";
        }

        public Client(string document, string name, DateOnly birthDate, Address address, string telephone, string email, decimal personIncome, string pdf)
        : base(document, name, birthDate, address, telephone, email)
        {
            PersonIncome = personIncome;
            PDF = pdf;  
        }

        
    }
}
