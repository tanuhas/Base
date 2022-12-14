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
    public class StorekeepersController : Controller
    {
        private readonly WarehouseDbContext _context;
        UserManager<User> _userManager;
        public StorekeepersController(WarehouseDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var storekeepers = _context.Packers.Include(m => m.Departament);
            return View(await storekeepers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storekeeper = await _context.Storekeepers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (storekeeper == null)
            {
                return NotFound();
            }

            return View(storekeeper);
        }
        //НАЧНИ ОТСЮДА!!!!!!!!!!!
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
        public async Task<IActionResult> Create([Bind("DepID,ID,Name,Age,UserID")] Storekeeper storekeeper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(storekeeper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["depID"] = new SelectList(_context.Departaments, "id", "id", storekeeper.depID);
            ViewData["UserID"] = new SelectList(_userManager.Users, "Id", "Id", storekeeper.UserID);
            return View(storekeeper);
        }

        // GET: Edit
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storekeeper = await _context.Storekeepers.FindAsync(id);
            if (storekeeper == null)
            {
                return NotFound();
            }
            return View(storekeeper);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepID,ID,Name,Age,UserID")] Storekeeper storekeeper)
        {
            if (id != storekeeper.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storekeeper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(storekeeper.ID))
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
            return View(storekeeper);
        }

        // GET:Delete
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storekeeper = await _context.Storekeepers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (storekeeper == null)
            {
                return NotFound();
            }

            return View(storekeeper);
        }

        // POST:Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var storekeeper = await _context.Storekeepers.FindAsync(id);
            _context.Storekeepers.Remove(storekeeper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Storekeepers.Any(e => e.ID == id);
        }
    }
}
