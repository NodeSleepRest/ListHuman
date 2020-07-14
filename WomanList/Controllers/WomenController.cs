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
    public class WomenController : Controller
    {
        private readonly test2dbContext _context;

        public WomenController(test2dbContext context)
        {
            _context = context;
        }

        // GET: Women
        public async Task<IActionResult> Index(int? page, string search, int? order, bool ordersort)
        {
            if (page == null)
            {
                page = 0;
            }
            int maxPage = 10;

            var women = _context.Woman.Select(m => m);
            if (!string.IsNullOrEmpty(search))
            {
                women = women.Where(b => b.仮名.Contains(search) || b.本名.Contains(search));
            }

            women = women
                .Skip(maxPage * page.Value).Take(maxPage)
                .Include(w => w.出身地Navigation).Include(w => w.居住地Navigation).Include(w => w.知り合った方法Navigation);

            if (ordersort)
            {
                switch (order)
                {
                    case 1:
                        women = women.Where(x => x.顔 > 0).OrderBy(x => x.顔);
                        break;
                    case 2:
                        women = women.Where(x => x.胸 > 0).OrderBy(x => x.胸);
                        break;
                    case 3:
                        women = women.Where(x => x.体型 > 0).OrderBy(x => x.体型);
                        break;
                }
            }
            else
            {
                switch (order)
                {
                    case 1:
                        women = women.Where(x => x.顔 > 0).OrderByDescending(x => x.顔);
                        break;
                    case 2:
                        women = women.Where(x => x.胸 > 0).OrderByDescending(x => x.胸);
                        break;
                    case 3:
                        women = women.Where(x => x.体型 > 0).OrderByDescending(x => x.体型);
                        break;
                }
            }

            if (page.Value > 0)
            {
                ViewData["prev"] = page.Value - 1;
            }
            if(women.Count() >= maxPage)
            {
                ViewData["next"] = page.Value + 1;
                if (_context.Woman.Skip(maxPage * (page.Value + 1)).Take(maxPage).Count() == 0)
                {
                    ViewData["next"] = null;
                }
            }

            ViewData["search"] = search;
            ViewData["order"] = order;
            ViewData["ordersort"] = ordersort;
            return View(await women.ToListAsync());
        }

        // GET: Women/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var woman = await _context.Woman
                .Include(w => w.出身地Navigation)
                .Include(w => w.居住地Navigation)
                .Include(w => w.知り合った方法Navigation)
                .Include(w => w.Dating)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (woman == null)
            {
                return NotFound();
            }

            return View(woman);
        }

        // GET: Women/Create
        public IActionResult Create()
        {
            ViewData["出身地"] = new SelectList(_context.Prefecture, "Id", "名称");
            ViewData["居住地"] = new SelectList(_context.Prefecture, "Id", "名称");
            ViewData["知り合った方法"] = new SelectList(_context.Method, "Id", "名称");
            return View();
        }

        // POST: Women/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,仮名,本名,年齢,居住地,出身地,職業,年収,顔,胸,体型,出会った日,知り合った方法,備考")] Woman woman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(woman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["出身地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.出身地);
            ViewData["居住地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.居住地);
            ViewData["知り合った方法"] = new SelectList(_context.Method, "Id", "名称", woman.知り合った方法);
            return View(woman);
        }

        // GET: Women/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var woman = await _context.Woman.FindAsync(id);
            if (woman == null)
            {
                return NotFound();
            }
            ViewData["出身地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.出身地);
            ViewData["居住地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.居住地);
            ViewData["知り合った方法"] = new SelectList(_context.Method, "Id", "名称", woman.知り合った方法);
            return View(woman);
        }

        // POST: Women/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,仮名,本名,年齢,居住地,出身地,職業,年収,顔,胸,体型,出会った日,知り合った方法,備考")] Woman woman)
        {
            if (id != woman.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(woman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WomanExists(woman.Id))
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
            ViewData["出身地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.出身地);
            ViewData["居住地"] = new SelectList(_context.Prefecture, "Id", "名称", woman.居住地);
            ViewData["知り合った方法"] = new SelectList(_context.Method, "Id", "名称", woman.知り合った方法);
            return View(woman);
        }

        // GET: Women/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var woman = await _context.Woman
                .Include(w => w.出身地Navigation)
                .Include(w => w.居住地Navigation)
                .Include(w => w.知り合った方法Navigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (woman == null)
            {
                return NotFound();
            }

            return View(woman);
        }

        // POST: Women/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var woman = await _context.Woman.FindAsync(id);
            _context.Woman.Remove(woman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WomanExists(int id)
        {
            return _context.Woman.Any(e => e.Id == id);
        }
    }
}
