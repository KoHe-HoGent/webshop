using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webshop_2.Data;

namespace webshop_2.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
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
        //FK cart, user
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
