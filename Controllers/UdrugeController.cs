using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Adoptly.Data;
using Adoptly.Models;

namespace Adoptly.Controllers
{
    public class UdrugeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UdrugeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Udruge
        public async Task<IActionResult> Index(int? id, int? ljubimacID)
        {
            var viewModel = new UdrugaView();
            viewModel.Udruge = await _context.Udruga
                  .Include(i => i.Posts)
                    .ThenInclude(i => i.Udruga)

                  .OrderBy(i => i.Naziv)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["UdrugaID"] = id.Value;
                Udruga udruga = viewModel.Udruge.Where(
                      i => i.Id == id.Value).Single();
                viewModel.Posts = udruga.Posts;
            }

            if (ljubimacID != null)
            {
                ViewBag.LjubimacID = ljubimacID.Value;
                var post = _context.Post.Select(b => b).Where(
                       x => x.Id == ljubimacID);
           
            }

            return View(viewModel);
        }

        // GET: Udruge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var udruga = await _context.Udruga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (udruga == null)
            {
                return NotFound();
            }

            return View(udruga);
        }

        // GET: Udruge/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Udruge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,SkraceniNaziv,Adresa,Grad,Tel,Mail,Web")] Udruga udruga)
        {
            if (ModelState.IsValid)
            {
                _context.Add(udruga);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(udruga);
        }

        // GET: Udruge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var udruga = await _context.Udruga.FindAsync(id);
            if (udruga == null)
            {
                return NotFound();
            }
            return View(udruga);
        }

        // POST: Udruge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,SkraceniNaziv,Adresa,Grad,Tel,Mail,Web")] Udruga udruga)
        {
            if (id != udruga.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(udruga);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UdrugaExists(udruga.Id))
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
            return View(udruga);
        }

        // GET: Udruge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var udruga = await _context.Udruga
                .FirstOrDefaultAsync(m => m.Id == id);
            if (udruga == null)
            {
                return NotFound();
            }

            return View(udruga);
        }

        // POST: Udruge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var udruga = await _context.Udruga.FindAsync(id);
            _context.Udruga.Remove(udruga);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UdrugaExists(int id)
        {
            return _context.Udruga.Any(e => e.Id == id);
        }
    }
}
