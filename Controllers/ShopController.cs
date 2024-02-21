using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webshop_2.Data;
using webshop_2.Models;
using webshop_2.Models.Interfaces;

namespace webshop_2.wwwroot
{
    public class ShopController : Controller
    {
        private readonly IShopItemRepository _repo;
        private readonly UserManager<AppUser> _usermanager;
        private readonly ApplicationDbContext _context;

        public ShopController(IShopItemRepository repo, UserManager<AppUser> usermanager, ApplicationDbContext context)
        {
            _repo = repo;
            _usermanager = usermanager;
            _context = context;
        }

        // GET: Shop
        public async Task<IActionResult> Index()
        {
            IEnumerable<ShopItem> shopitems = await _repo.GetAll();
            return View(shopitems);
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ShopItem shopitem = await _repo.GetById(id);
            if (shopitem == null) return NotFound();

            return View(shopitem);
        }

        [Authorize(Roles = UserRoles.Admin)]
        // GET: Shop/Create 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,Description,Producer")] ShopItem shopItem)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(shopItem);
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        // GET: Shop/Edit/5
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var shopItem = await _repo.GetById(id);
            if (shopItem == null) return NotFound();
            return View(shopItem);
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,Description,Producer, IsDeleted")] ShopItem shopItem)
        {
            if (id != shopItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _repo.Update(shopItem);
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var shopitem = await _repo.GetById(id);
            if (shopitem == null) return NotFound();
            return View(shopitem);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id, [Bind("Id,Name,Price,Stock,Description,Producer, IsDeleted")] ShopItem shopItem)
        {
            if (id != shopItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _repo.Delete(shopItem);
                return RedirectToAction(nameof(Index));
            }
            return View(shopItem);
        }

        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            int quantity = Convert.ToInt32(HttpContext.Request.Form["qty"].ToString());
            var shopitem = await _repo.GetById(id);
            if (shopitem == null) return NotFound();

            if (quantity < 1 || quantity > shopitem.Stock) return Problem("Amount's range is between 1 and item's stock");

            var userid = (await _usermanager.GetUserAsync(User)).Id;
            var user = await _context.Users.Where(u => u.Id == userid).Include(u => u.Cart).ThenInclude(c => c.CartItems).FirstAsync();
            user.Cart.AddShopItem(shopitem, quantity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
