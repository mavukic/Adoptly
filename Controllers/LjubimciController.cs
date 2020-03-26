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
    public class LjubimciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LjubimciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ljubimci
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ljubimac.Include(l => l.Skloniste).Where(l => l.SklonisteId != null); 
            //treba prikazati ljubimce koji nemaju posvajatelja a u sklonistu su 
            
            //applicationDbContext.Include(l => l.PosvajteljId).Where(l => l.PosvajteljId ==null);
         
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Izgubljeni()
        {
            var applicationDbContext = _context.Ljubimac.Include(l => l.Skloniste).Where(l=>l.SklonisteId==null);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Ljubimci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Skloniste)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // GET: Ljubimci/Create
        public IActionResult Create()
        {
            ViewData["SklonisteId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Id");
            ViewData["PosvajateljId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Prezime");
            return View();
        }

        // POST: Ljubimci/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ime,Content,SklonisteId,Vrsta,Opis,Godine,PosvajteljId")] Ljubimac ljubimac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ljubimac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SklonisteId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Id", ljubimac.SklonisteId);
          
            return View(ljubimac);
        }

        // GET: Ljubimci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac.FindAsync(id);
            if (ljubimac == null)
            {
                return NotFound();
            }
            ViewData["SklonisteId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Naziv", ljubimac.SklonisteId);

            return View(ljubimac);
        }

        // POST: Ljubimci/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ime,Content,SklonisteId,Vrsta,Opis,Godine,PosvajteljId")] Ljubimac ljubimac)
        {
            if (id != ljubimac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ljubimac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LjubimacExists(ljubimac.Id))
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
            ViewData["SklonisteId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Id", ljubimac.SklonisteId);
            ViewData["PosvajateljId"] = new SelectList(_context.Set<Skloniste>(), "Id", "Prezime", ljubimac.PosvajteljId);
            return View(ljubimac);
        }

        // GET: Ljubimci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ljubimac = await _context.Ljubimac
                .Include(l => l.Skloniste)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ljubimac == null)
            {
                return NotFound();
            }

            return View(ljubimac);
        }

        // POST: Ljubimci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ljubimac = await _context.Ljubimac.FindAsync(id);
            _context.Ljubimac.Remove(ljubimac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LjubimacExists(int id)
        {
            return _context.Ljubimac.Any(e => e.Id == id);
        }
    }
}
