using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webshop_2.Models;

namespace webshop_2.Data
{
    public class AppUser : IdentityUser
    {
        [Key]
        public override string Id { get; set; }

        //customer info
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [DisplayName("Street address")]
        [Required]
        [MinLength(5)]
        public string StreetAddress { get; set; }
        [Required]
        [MinLength(2)]
        public string City { get; set; }
        [Required]
        [Display(Prompt = "Postal code")]
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }
        [Required]
        [MinLength(2)]
        public string Country { get; set; }
        [Required]
        [Display(Prompt = "Credit Card Number")]
        [CreditCard]
        public string CardNumber { get; set; }

        //FKs
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public List<Order> Orders { get; set; }

        public AppUser()
        {
            Orders = new List<Order>();
            Cart = new Cart();
            CartId = Cart.Id;
        }
    }
}
