using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreFinalApp.Domain;
using AspNetCoreFinalApp.Domain.Entities;

namespace AspNetCoreFinalApp.Controllers
{
    public class MycarsController : Controller
    {
        private readonly EFDataContext _context;

        public MycarsController(EFDataContext context)
        {
            _context = context;
        }

        // GET: Mycars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cars.ToListAsync());
        }

        // GET: Mycars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appCar = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appCar == null)
            {
                return NotFound();
            }

            return View(appCar);
        }

        // GET: Mycars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mycars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Developer,Model,Price,Image,Year,Id,DateCreate,DateModified,DateDelete")] AppCar appCar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appCar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appCar);
        }

        // GET: Mycars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appCar = await _context.Cars.FindAsync(id);
            if (appCar == null)
            {
                return NotFound();
            }
            return View(appCar);
        }

        // POST: Mycars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Developer,Model,Price,Image,Year,Id,DateCreate,DateModified,DateDelete")] AppCar appCar)
        {
            if (id != appCar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appCar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppCarExists(appCar.Id))
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
            return View(appCar);
        }

        // GET: Mycars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appCar = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appCar == null)
            {
                return NotFound();
            }

            return View(appCar);
        }

        // POST: Mycars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appCar = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(appCar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppCarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
