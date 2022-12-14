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
    public class PackersController : Controller
    {
        private readonly WarehouseDbContext _context;
        UserManager<User> _userManager;
        public PackersController(WarehouseDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var packers = _context.Packers.Include(m => m.Departament);
            return View(await packers.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packer = await _context.Packers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (packer == null)
            {
                return NotFound();
            }

            return View(packer);
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
        public async Task<IActionResult> Create([Bind("DepID,ID,Name,Age,UserID")] Packer packer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["depID"] = new SelectList(_context.Departaments, "id", "id", packer.depID);
            ViewData["UserID"] = new SelectList(_userManager.Users, "Id", "Id", packer.UserID);
            return View(packer);
        }

        // GET: Edit
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packer = await _context.Packers.FindAsync(id);
            if (packer == null)
            {
                return NotFound();
            }
            return View(packer);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepID,ID,Name,Age,UserID")] Packer packer)
        {
            if (id != packer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(packer.ID))
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
            return View(packer);
        }

        // GET:Delete
        [Authorize(Roles = "Администратор")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packer = await _context.Packers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (packer == null)
            {
                return NotFound();
            }

            return View(packer);
        }

        // POST:Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packer = await _context.Packers.FindAsync(id);
            _context.Packers.Remove(packer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Packers.Any(e => e.ID == id);
        }
    }
}
