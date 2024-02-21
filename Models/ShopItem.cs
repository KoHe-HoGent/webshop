using System.ComponentModel.DataAnnotations;

namespace webshop_2.Models
{
    public class ShopItem
    {
        [Key]
        public int Id { get; set; }
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
        public string Name { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Range is set between 0 and maximum")]
        public double Price { get; set; }
        //bewust geen range bij stock. menselijke fout kan negatieve stock veroorzaken (in magazijn te weinig fysieke stock tellen)
        public int Stock { get; set; }
        [MinLength(5, ErrorMessage = "Minimum length is 5 characters")]
        public string Description { get; set; }
        [MinLength(2, ErrorMessage = "Minimum length is 2 characters")]
        public string Producer { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
