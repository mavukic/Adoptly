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
    public class SklonistaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SklonistaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sklonista
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skloniste.ToListAsync());
        }

        // GET: Sklonista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skloniste = await _context.Skloniste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skloniste == null)
            {
                return NotFound();
            }

            return View(skloniste);
        }

        // GET: Sklonista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sklonista/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naziv,SkraceniNaziv,Adresa,Grad,Tel,Mail,Web")] Skloniste skloniste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(skloniste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(skloniste);
        }

        // GET: Sklonista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skloniste = await _context.Skloniste.FindAsync(id);
            if (skloniste == null)
            {
                return NotFound();
            }
            return View(skloniste);
        }

        // POST: Sklonista/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naziv,SkraceniNaziv,Adresa,Grad,Tel,Mail,Web")] Skloniste skloniste)
        {
            if (id != skloniste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skloniste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SklonisteExists(skloniste.Id))
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
            return View(skloniste);
        }

        // GET: Sklonista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skloniste = await _context.Skloniste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skloniste == null)
            {
                return NotFound();
            }

            return View(skloniste);
        }

        // POST: Sklonista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skloniste = await _context.Skloniste.FindAsync(id);
            _context.Skloniste.Remove(skloniste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SklonisteExists(int id)
        {
            return _context.Skloniste.Any(e => e.Id == id);
        }
    }
}
