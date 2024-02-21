using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace webshop_2.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartrepo;
        private readonly ICartItemRepository _cartitemrepo;
        private readonly IOrderRepository _orderrepo;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _usermanager;

        public CartController(ICartRepository cartrepo, ICartItemRepository cartitemrepo, ApplicationDbContext context, UserManager<AppUser> usermanager, IOrderRepository orderrepo)
        {
            _cartrepo = cartrepo;
            _cartitemrepo = cartitemrepo;
            _context = context;
            _usermanager = usermanager;
            _orderrepo = orderrepo;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            //haal user op, include actieve cart
            var userid = (await _usermanager.GetUserAsync(User)).Id;
            var user = await _context.Users.Where(u => u.Id == userid).Include(u => u.Cart).ThenInclude(c => c.CartItems).ThenInclude(ci => ci.ShopItem).FirstAsync();

            return View(user.Cart);
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int id)
        {
            CartItem cartitem = await _cartitemrepo.GetById(id);
            if (cartitem == null)
            {
                return NotFound();
            }

            return View(cartitem);
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var cartitem = await _cartitemrepo.GetById(id);
            if (cartitem == null)
            {
                return NotFound();
            }
            return View(cartitem);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Amount,Price,ShopItemId,CartId,IsDeleted")] CartItem cartitem)
        {
            if (id != cartitem.Id)
            {
                return NotFound();
            }

            _cartitemrepo.Update(cartitem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cartitem = await _cartitemrepo.GetById(id);
            if (cartitem == null)
            {
                return NotFound();
            }
            return View(cartitem);
        }

        // POST: Cart/Delete/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(CartItem cartitem)
        {
            if (cartitem == null)
            {
                return NotFound();
            }

            await _cartitemrepo.Delete(cartitem.Id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("ClearCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart(int id)
        {
            var cart = await _cartrepo.GetById(id);
            if (cart == null) return NotFound();

            cart.ClearCart();
            _cartrepo.Update(cart);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost, ActionName("SendOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOrder(int id)
        {
            //haal user op met cart
            var userid = (await _usermanager.GetUserAsync(User)).Id;
            var user = await _context.Users.Include(u => u.Cart).ThenInclude(c => c.CartItems).FirstAsync(u => u.Id == userid);
            if (user == null) { return NotFound(); }

            //maak order object
            var order = new Order()
            {
                CartId = id,
                AppUserId = userid,
                StreetAddress = user.StreetAddress,
                City = user.City,
                PostalCode = user.PostalCode,
                Country = user.Country
            };

            //stuur naar database
            var orderResult = _orderrepo.Add(order);
            //check error
            if (!orderResult) return View(id);

            //order in database, dus cart krijgt orderid
            user.Cart.OrderId = order.Id;
            //update cart
            _cartrepo.Update(user.Cart);

            //user krijgt nieuwe cart, oude cart losgekoppeld van user (nu gelinkt aan order)
            user.Cart = new Cart();
            user.CartId = user.Cart.Id;
            //update user
            await _usermanager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
    }
}
