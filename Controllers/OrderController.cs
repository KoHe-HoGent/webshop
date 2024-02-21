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
using Webshop3.Repository;

namespace webshop_2.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _repo;
        private readonly UserManager<AppUser> _usermanager;

        public OrderController(ApplicationDbContext context, IOrderRepository repo, UserManager<AppUser> usermanager)
        {
            _context = context;
            _repo = repo;
            _usermanager = usermanager;
        }

        // GET: Order
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Order> orders = await _repo.GetAll();
            if (orders == null) { return NotFound(); }
            return View(orders);
        }

        public async Task<IActionResult> Index()
        {
            var userid = (await _usermanager.GetUserAsync(User)).Id;
            var user = await _context.Users.Include(u => u.Orders).ThenInclude(o => o.Cart).ThenInclude(c => c.CartItems).ThenInclude(ci => ci.ShopItem).FirstAsync(u => u.Id == userid);

            if (user == null) return NotFound();

            return View(user.Orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _repo.GetById(id);
            if (order == null) return NotFound();

            return View(order);
        }

        // GET: Order/Delete/5
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _repo.GetById(id);
            if (order == null) return NotFound();

            return View(order);
        }

        // POST: Order/Delete/5
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }

            await _repo.Delete(order.Id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _repo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            _repo.Update(order);
            return RedirectToAction(nameof(Index));
        }
    }
}
