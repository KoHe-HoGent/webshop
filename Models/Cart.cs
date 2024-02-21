using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webshop_2.Data;

namespace webshop_2.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        //FKs (een cart heeft meerdere cartitems, een cart behoort tot 1 order)
        public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
        public int? OrderId { get; set; }
        public Order? Order { get; set; }
        //[ForeignKey("AppUser")]
        //public string AppUserId { get; set; }
        //public AppUser AppUser { get; set; }

        public void AddShopItem(ShopItem shopitem, int amount)
        {
            var i = CartItems.FirstOrDefault(i =>  i.ShopItemId == shopitem.Id);
            if (i == null)
            {
                CartItems.Add(new CartItem()
                {
                    ShopItemId = shopitem.Id,
                    ShopItem = shopitem,
                    Amount = amount,
                    Price = shopitem.Price,
                });
            }
            else {
                i.Price = shopitem.Price;
                i.Amount = amount;
                i.IsDeleted = false;
            };
            
        }

        public void RemoveCartItem(CartItem cartitem)
        {
            cartitem.IsDeleted = true;
        }

        public void ClearCart()
        {
            foreach (var i in CartItems)
            {
                i.IsDeleted = true;
            }
        }
    }
}
