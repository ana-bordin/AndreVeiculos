using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Car
    {
        [Key]
        public string LicensePlate { get; set; }
        public string Name { get; set; }
        public int ModelYear { get; set; }
        public int ManufactureYear { get; set; }
        public string Color { get; set; }
        public bool CarSold { get; set; }

        public override string ToString()
        {
            return $"LicensePlate: {LicensePlate}, Name: {Name}, ModelYear: {ModelYear}, ManufactureYear: {ManufactureYear}, Color: {Color}, CarSold: {CarSold}";
        }
    }
}
