using System.ComponentModel.DataAnnotations;

namespace webshop_2.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Range is set between 0 and maximum")]
        public int Amount { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Range is set between 0 and maximum")]
        public double Price { get; set; }
        public bool IsDeleted { get; set; } = false;

        //FKs (een cartitem heeft 1 cart en een 1 shopitem)
        public int ShopItemId { get; set; }
        public ShopItem ShopItem { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
    }
}
