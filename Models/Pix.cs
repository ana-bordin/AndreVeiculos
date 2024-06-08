using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Pix
    {
        [Key]
        public int Id { get; set; }
        public string Key { get; set; }
        public PixType PixType { get; set; }
    }
}
