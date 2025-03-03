﻿using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;


namespace ServiceLayer.Services
{
    public class BookService : IBook
    {

        private readonly LibraryDbContext _context;
        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public  async Task<Book> Create(Book book)
        {
            var shelf = await _context.Shelfs.FirstOrDefaultAsync(s => s.Id == book.ShelfId);
            shelf.BookCount = shelf.BookCount + 1;
            _context.Entry(shelf).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            _context.Entry(book).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return book;

        }



        public async Task<List<Book>> GetAll()
        {
            var Books =await _context.Books.Where(x=>x.IsDeleted == false).Include(b => b.Shelf).ToListAsync();
            var book =  Books.Where(x => x.Shelf.IsDeleted == false).ToList();
            return book;
        }
		public async Task<List<Book>> GetAllArchive()
		{
			var Books = await _context.Books.Where(x => x.IsDeleted == true).Include(b => b.Shelf).ToListAsync();
			var Bookfalse = await _context.Books.Where(x => x.IsDeleted == false).Include(b => b.Shelf).ToListAsync();
			Books.AddRange(Bookfalse.Where(x => x.Shelf.IsDeleted == true).ToList());
			return Books;
		}

		public  byte[] ConvertIFormFileToByteArray(IFormFile formFile)
        {
            using (var stream = new MemoryStream())
            {
                formFile.CopyTo(stream);
                return stream.ToArray();
            }
        }
       
            public async Task<Book> GetBook(int Id)
        {
            var singleBook = await _context.Books.Include(b => b.Shelf).Where(a=> a.Id == Id).FirstOrDefaultAsync();
            return singleBook;
        }
        public async Task<Book> Edit(int Id, Book EditedBook,IFormFile? pdfFile)
        {

            var existingBook = await GetBook(Id);

            if (existingBook == null)
            {
                // Handle the case when the book is not found
                throw new Exception("Book not found");
            }
            if (existingBook.ShelfId != EditedBook.ShelfId)
            {
                var shelf = await _context.Shelfs.FirstOrDefaultAsync(s => s.Id == EditedBook.ShelfId);
                shelf.BookCount = shelf.BookCount + 1;
                _context.Entry(shelf).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                var shelf2 = await _context.Shelfs.FirstOrDefaultAsync(s => s.Id == existingBook.ShelfId);
                shelf2.BookCount = shelf2.BookCount - 1;
                _context.Entry(shelf2).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            // Update the properties of the existing book with the edited values
            existingBook.Aurther = EditedBook.Aurther;
            existingBook.Price = EditedBook.Price;
            existingBook.ShelfId = EditedBook.ShelfId;
            existingBook.Quantity = EditedBook.Quantity;

            existingBook.Name = EditedBook.Name;
          

            if (pdfFile != null)
            {
                existingBook.PDF = ConvertIFormFileToByteArray(pdfFile);
            }
        
            // Mark the entity as modified
            _context.Entry(existingBook).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<Book> Delete(int Id)
        {
            var book = await GetBook(Id);
            var shelf = await _context.Shelfs.FirstOrDefaultAsync(s => s.Id == book.ShelfId);
            shelf.BookCount = shelf.BookCount - 1;
            _context.Entry(shelf).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            book.IsDeleted = true;
            _context.Entry(book).State = EntityState.Modified;
           await _context.SaveChangesAsync();
            return book;
        }
		public async Task<Book> DeletePermenetly(int Id)
		{
			var book = await GetBook(Id);
			
			_context.Entry(book).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
			return book;
		}
		public async Task<Book> Retrive(int Id)
		{
			var book = await GetBook(Id);
			var shelf = await _context.Shelfs.FirstOrDefaultAsync(s => s.Id == book.ShelfId);
			shelf.BookCount = shelf.BookCount + 1;
			_context.Entry(shelf).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			book.IsDeleted = false;
			_context.Entry(book).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return book;
		}


	}
}
