using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Buy
    {
        public int Id { get; set; }
        public Car Car { get; set; }
        public double Value { get; set; }
        public DateOnly Date { get; set; }
    }
}
