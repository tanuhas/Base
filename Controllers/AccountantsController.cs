using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication5.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class AccountantsController : Controller
    {
        private readonly WarehouseDbContext _context;
        UserManager<User> _userManager;
        public AccountantsController(WarehouseDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var accountants = _context.Accountants.Include(m => m.Departament);
            return View(await accountants.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountant = await _context.Accountants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountant == null)
            {
                return NotFound();
            }

            return View(accountant);
        }

        // GET:Create
        [Authorize(Roles = "Администратор")]
        public IActionResult Create()
        {
            ViewData["DepID"] = new SelectList(_context.Departaments, "id", "name");
            ViewData["UserID"] = new SelectList(_userManager.Users, "Id", "UserName");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepID,ID,Name,Age,UserID")] Accountant accountant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accountant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["depID"] = new SelectList(_context.Departaments, "id", "id", accountant.depID);
            ViewData["UserID"] = new SelectList(_userManager.Users, "Id", "Id", accountant.UserID);
            return View(accountant);
        }

        // GET: Edit
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountant = await _context.Accountants.FindAsync(id);
            if (accountant == null)
            {
                return NotFound();
            }
            return View(accountant);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepID,ID,Name,Age,UserID")] Accountant accountant)
        {
            if (id != accountant.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(accountant.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(accountant);
        }

        // GET:Delete
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accountant = await _context.Accountants
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accountant == null)
            {
                return NotFound();
            }

            return View(accountant);
        }

        // POST:Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accountant = await _context.Accountants.FindAsync(id);
            _context.Accountants.Remove(accountant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Accountants.Any(e => e.ID == id);
        }
    }
}
