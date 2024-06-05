using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Employee : People
    {
        public PositionCompany PositionCompany { get; set; }
        public Decimal CommissionPercentage { get; set; }
        public Decimal Commission { get; set; }
    }
}
