using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PixType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
