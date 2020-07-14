using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WomanList.Models;

namespace WomanList.Controllers
{
    public class DatingsController : Controller
    {
        private readonly test2dbContext _context;

        public DatingsController(test2dbContext context)
        {
            _context = context;
        }

        // GET: Datings
        public async Task<IActionResult> Index(int? page)
        {
            if (page == null)
            {
                page = 0;
            }
            int maxPage = 2;

            var datings = _context.Dating
                .Skip(maxPage * page.Value).Take(maxPage)
                .Include(d => d.女性);

            if (page.Value > 0)
            {
                ViewData["prev"] = page.Value - 1;
            }
            if (datings.Count() >= maxPage)
            {
                ViewData["next"] = page.Value + 1;
                if (_context.Woman.Skip(maxPage * (page.Value + 1)).Take(maxPage).Count() == 0)
                {
                    ViewData["next"] = null;
                }
            }

            return View(await datings.ToListAsync());
        }

        // GET: Datings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dating = await _context.Dating
                .Include(d => d.女性)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dating == null)
            {
                return NotFound();
            }

            return View(dating);
        }

        // GET: Datings/Create
        public IActionResult Create()
        {
            ViewData["女性id"] = new SelectList(_context.Woman, "Id", "仮名");
            return View();
        }

        // POST: Datings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,女性id,場所,費用,時間帯,備考")] Dating dating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["女性id"] = new SelectList(_context.Woman, "Id", "仮名", dating.女性id);
            return View(dating);
        }

        // GET: Datings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dating = await _context.Dating.FindAsync(id);
            if (dating == null)
            {
                return NotFound();
            }
            ViewData["女性id"] = new SelectList(_context.Woman, "Id", "仮名", dating.女性id);
            return View(dating);
        }

        // POST: Datings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,女性id,場所,費用,時間帯,備考")] Dating dating)
        {
            if (id != dating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DatingExists(dating.Id))
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
            ViewData["女性id"] = new SelectList(_context.Woman, "Id", "仮名", dating.女性id);
            return View(dating);
        }

        // GET: Datings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dating = await _context.Dating
                .Include(d => d.女性)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dating == null)
            {
                return NotFound();
            }

            return View(dating);
        }

        // POST: Datings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dating = await _context.Dating.FindAsync(id);
            _context.Dating.Remove(dating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DatingExists(int id)
        {
            return _context.Dating.Any(e => e.Id == id);
        }
    }
}
