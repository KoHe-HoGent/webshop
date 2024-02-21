using Microsoft.EntityFrameworkCore;
using webshop_2.Models;

namespace Webshop2.Data
{
    public static class ModelBuilderExtensions
    {
        public static void SeedShopItems(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShopItem>().HasData(
                new ShopItem
                {
                    Id = 1,
                    Name = "Nepenthes - the Monkey Cups",
                    Description = "Nepenthes, a native of Southeast Asia and Australia, forms pitchers (cups) that hang from trees.",
                    Price = 14.99,
                    Stock = 5,
                    Producer = "Plants n Stuff"
                },
                new ShopItem
                {
                    Id = 2,
                    Name = "Drosophyllum",
                    Description = "shrub-like carnivorous plant, normally growing to be 40-50 cm in height",
                    Price = 8.49,
                    Stock = 23,
                    Producer = "Plants n stuff"
                },
                new ShopItem
                {
                    Id = 3,
                    Name = "Dionaea muscipula - The Venus Flytrap",
                    Description = "The steel trap of Dionaea is hardly as powerful as the ones set by trappers for wolves, beavers or bears, but it is just as effective at catching its own small prey.",
                    Price = 10.99,
                    Stock = 10,
                    Producer = "Plants r us"
                },
                new ShopItem
                {
                    Id = 4,
                    Name = "Drosera - the Sundews",
                    Description = "the sundew relies on first trapping its prey with its sticky, glandular hairs.",
                    Price = 10.99,
                    Stock = 10,
                    Producer = "WeHavePlantz"
                },
                new ShopItem
                {
                    Id = 5,
                    Name = "Cephalotus follicularis - the Albany Pitcher Plant",
                    Description = "It grows from underground rhizomes and its evergreen leaves hug the ground. It isn't related to other pitcher plants, although it has some features that resemble them.",
                    Price = 15.99,
                    Stock = 6,
                    Producer = "Plants n stuff"
                },
                new ShopItem
                {
                    Id = 6,
                    Name = "Sarracenia - the Pitcher Plants",
                    Description = " flies easily become victims of the pitfall trap when they seek potential food inside.",
                    Price = 8.99,
                    Stock = 17,
                    Producer = "WeHavePlantz"
                }
            );
        }
    }
}
