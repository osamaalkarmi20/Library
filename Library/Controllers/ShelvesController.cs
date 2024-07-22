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
using System.Drawing;

namespace Library.Controllers
{
    public class ShelvesController : Controller
    {
        private readonly LibraryDbContext _context;
        private readonly IShelf _shelf;
		private readonly IBook _book; 

        public ShelvesController(LibraryDbContext context, IShelf shelf, IBook book)
        {
            _context = context;
            _book = book;
            _shelf = shelf;
        
        }

        // GET: Shelves
        public async Task<IActionResult> Index()
        {
            var Shelves = await _shelf.GetAll();
            return View(Shelves);

        }

        public async Task<IActionResult> Dashboard()
        {
            //await _email.sendEmail();
           
            var Shelves = await _shelf.GetAll();
            var Books = await _book.GetAll();
			List<string> Colors = new List<string>();
			foreach (var item in Books) { 
            Random r = new Random();
            var a = Color.FromArgb(r.Next(256), r.Next(256), r.Next(256));

                string z = $"rgb({a.R}, {a.G}, {a.B})";
				Colors.Add(z);


		}

			List<int> BookQuantitys = new List<int>();
			List<string> BookLabels = new List<string>();
			List<string> ShelfLabels = new List<string>();
			List<int> ShelfBooksCount = new List<int>();
			foreach (var item in Books)
			{
				BookLabels.Add(item.Name);
				BookQuantitys.Add(item.Quantity);

			}
            foreach (var item in Shelves)
            {
                ShelfLabels.Add(item.Name);
                ShelfBooksCount.Add(item.BookCount);
            }

			var BL = BookLabels.ToArray();
			var BQ = BookQuantitys.ToArray();
            var SL = ShelfLabels.ToArray();
            var SBC = ShelfBooksCount.ToArray();
           var CLR= Colors.ToArray();
			ViewData["BookLabels"] = BL;
			ViewData["BookQuantitys"] = BQ;
			ViewData["Colors"] = CLR;
			ViewData["ShelfLabels"] = SL;
			ViewData["ShelfBooksCount"] = SBC;
			return View(Shelves);

		}

		public async Task<IActionResult> Archive()
        {
            var Shelves = await _shelf.GetAllArchive();
            return View(Shelves);

        }
		public async Task<IActionResult> Retrive(int Id)
		{
			var Shelves = await _shelf.Retrive(Id);
			return RedirectToAction("Archive","Shelves" );

		}
		public async Task<IActionResult> DeletePermently(int Id)
		{
			var Shelves = await _shelf.DeletePermenetly(Id);
			return RedirectToAction("Archive", "Shelves");

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

        // GET: Shelves/Create D
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
