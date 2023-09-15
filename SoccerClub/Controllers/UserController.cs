using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerClub.Models;

namespace SoccerClub.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin1
        public async Task<IActionResult> Index()
        {
              return _context.login != null ? 
                          View(await _context.login.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.login'  is null.");
        }

        // GET: Admin1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.login == null)
            {
                return NotFound();
            }

            var login = await _context.login
                .FirstOrDefaultAsync(m => m.ID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // GET: Admin1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,username,password")] login login)
        {
            if (ModelState.IsValid)
            {
                _context.Add(login);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(login);
        }

        // GET: Admin1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.login == null)
            {
                return NotFound();
            }

            var login = await _context.login.FindAsync(id);
            if (login == null)
            {
                return NotFound();
            }
            return View(login);
        }

        // POST: Admin1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,username,password")] login login)
        {
            if (id != login.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!loginExists(login.ID))
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
            return View(login);
        }

        // GET: Admin1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.login == null)
            {
                return NotFound();
            }

            var login = await _context.login
                .FirstOrDefaultAsync(m => m.ID == id);
            if (login == null)
            {
                return NotFound();
            }

            return View(login);
        }

        // POST: Admin1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.login == null)
            {
                return Problem("Entity set 'ApplicationDbContext.login'  is null.");
            }
            var login = await _context.login.FindAsync(id);
            if (login != null)
            {
                _context.login.Remove(login);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool loginExists(int id)
        {
          return (_context.login?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
