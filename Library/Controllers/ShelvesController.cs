using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DataLayer.Models;
using ServiceLayer.Interface;
using NuGet.Protocol;
using DataLayer.Models.DTO;

namespace Library.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly IShelf _shelf;

        public ShelvesController(LibraryDbContext context, IShelf shelf)
        {
            _context = context;
            _shelf = shelf;
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var Shelves = await _shelf.GetAll();
            return View(Shelves);

        }

        // GET: Shelves/Details/5
        public async Task<IActionResult> Details(int id)
        {

            var shelf = await _shelf.GetShelf(id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // GET: Shelves/Create
        public async Task<IActionResult> Create()
        {
            var lOOKUPCATEGORY = await _context.LookUpCategories.Where(X => X.Code == "1").Include(C => C.lookUps).FirstOrDefaultAsync();
            var lOOKUPS = lOOKUPCATEGORY.lookUps;
            ViewData["Type"] = new SelectList(lOOKUPS, "Id", "Name");
            return View();
        }

        // POST: Shelves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CreationShelf shelf)
        {
            if (ModelState.IsValid)
            {
                var newShelf = new Shelf()
                {

                    Id = shelf.Id,
                    Name = shelf.Name,

                    BookCount = 0,
                    IsActived = shelf.IsActived,
                    Type = await _context.LookUps.FindAsync(shelf.Type)
                };
                await _shelf.Create(newShelf);
                return RedirectToAction(nameof(Index));
            }
            return View(shelf);
        }

        // GET: Shelves/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var lOOKUPCATEGORY = await _context.LookUpCategories.Where(X => X.Code == "1").Include(C => C.lookUps).FirstOrDefaultAsync();
            var lOOKUPS = lOOKUPCATEGORY.lookUps;
            ViewData["Type"] = new SelectList(lOOKUPS, "Id", "Name");

            var shelf = await _shelf.GetShelf(id);
            if (shelf == null)
            {
                return NotFound();
            }
            return View(shelf);
        }

        // POST: Shelves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] CreationShelf shelf)
        {
            if (id != shelf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var newShelf = new Shelf()
                {

                    Id = id,
                    Name = shelf.Name,

                  
                    IsActived = shelf.IsActived,
                    Type = await _context.LookUps.FindAsync(shelf.Type)
                };
                await _shelf.Edit(id, newShelf);


                return RedirectToAction(nameof(Index));
            }
            return View(shelf);
        }

        // GET: Shelves/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            

            var shelf = await _shelf.GetShelf(id);
            if (shelf == null)
            {
                return NotFound();
            }

            return View(shelf);
        }

        // POST: Shelves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shelfs == null)
            {
                return Problem("Entity set 'LibraryDbContext.Shelfs'  is null.");
            }
            var shelf = await _shelf.GetShelf(id);
            if (shelf != null)
            {
                await _shelf.Delete(id);
            }
            
      
            return RedirectToAction(nameof(Index));
        }

      
    }
}
