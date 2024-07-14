
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataLayer.Models;
using ServiceLayer.Interface;
using Microsoft.AspNetCore.Http.HttpResults;


namespace Library.Controllers
{
    public class BooksController : Controller
    {

        private readonly IBook _book;
        private readonly IShelf _Shelf;

        public BooksController(IBook book, IShelf shelf)
        {

            _book = book;
            _Shelf = shelf;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {

            var Books = await _book.GetAll();
            return View(Books);
        }
		public async Task<IActionResult> Archive()
		{

			var Books = await _book.GetAllArchive();
			return View(Books);
		}
		public async Task<IActionResult> Retrive(int Id)
		{
			var Shelves = await _book.Retrive(Id);
			return RedirectToAction("Archive", "Books");

		}
		public async Task<IActionResult> DeletePermently(int Id)
		{
			var Shelves = await _book.DeletePermenetly(Id);
			return RedirectToAction("Archive", "Books");

		}
		// GET: Books/Details/5
		public async Task<IActionResult> Details(int id)
        {
            var book = await _book.GetBook(id);
          
            return View(book);
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var Shelfs = await _Shelf.GetAll();
            ViewData["ShelfId"] = new SelectList(Shelfs, "Id", "Name");
            return View();
        }
        [HttpGet]
        [Route("CreateFromShelf/{id}")]
        public async Task<IActionResult> CreateFromShelf(int id)
        {
            var Shelfs = await _Shelf.GetShelf(id);
            ViewData["ShelfId"] = id;
            return View();
        }

        [HttpPost]
        [Route("CreateFromShelf/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromShelf(IFormFile pdfFile, [FromForm] Book book)

        {
            var receivedShelfId = book.ShelfId;
            
            if (book.PDFFile.ContentType != "application/pdf" && book.PDFFile.ContentType == null)
            {
                ModelState.AddModelError("PDFFile", "Only PDF files are allowed.");
                var Shelfs = await _Shelf.GetAll();
                ViewData["ShelfId"] = new SelectList(Shelfs, "Id", "Name");
                return View();

            }
            book.PDF = _book.ConvertIFormFileToByteArray(pdfFile);
            if (ModelState.IsValid)
            {
                await _book.Create(book);
                return RedirectToAction("Details", "Shelves", new { id = receivedShelfId });
            }
            return View();



        }
        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile pdfFile, [FromForm] Book book)
        {
            if (book.PDFFile == null || book.PDFFile.ContentType != "application/pdf"  )
            {
                ModelState.AddModelError("PDFFile", "Only PDF files are allowed.");
                var Shelfs = await _Shelf.GetAll();
                ViewData["ShelfId"] = new SelectList(Shelfs, "Id", "Name");
                return View();
            }
            book.PDF = _book.ConvertIFormFileToByteArray(pdfFile);
            if (ModelState.IsValid)
            {
                await _book.Create(book);
                return RedirectToAction("Index", "Books");
            }
            return View();



        }
       

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int id)
        {


            var book = await _book.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            var Shelfs = await _Shelf.GetAll();
            ViewData["ShelfId"] = new SelectList(Shelfs, "Id", "Name", book.ShelfId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( [FromForm] Book book, IFormFile? pdfFile)
        {
            var Shelfs = await _Shelf.GetAll();
            ViewData["ShelfId"] = new SelectList(Shelfs, "Id", "Name", book.ShelfId);
            if (pdfFile != null)
            {
                if (book.PDFFile.ContentType != "application/pdf" || book.PDFFile == null)
                {
                    ModelState.AddModelError("PDFFile", "Only PDF files are allowed.");

                    var books = await _book.GetBook(book.Id);
                  
                    return View(books);

                }
            }
            if (ModelState.IsValid)
            {
                var books = await _book.GetBook(book.Id);

                var bookedited = await _book.Edit(books.Id, book ,pdfFile);


                return RedirectToAction(nameof(Index));

            }
            
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int id)
        {


            var book = await _book.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var book = await _book.GetBook(id);
            if (book != null)
            {
                await _book.Delete(id);
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DownloadPDF(int id)
        {
            var book = await _book.GetBook(id);
            if (book == null || book.PDF == null)
            {
                return NotFound();
            }

            return File(book.PDF, "application/pdf", $"{book.Name}.pdf");
        }
    }
}
