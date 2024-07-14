using DataLayer.Data;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLayer.Services
{
    public class ShelfService : IShelf
    {
        private readonly LibraryDbContext _context;
        public ShelfService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Shelf> Create(Shelf shelf)
        {
            _context.Entry(shelf).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return shelf;

        }



        public async Task<List<Shelf>> GetAll()
        {
            var Shelfs = await _context.Shelfs.Where(x => x.IsDeleted == false).Select( tr => new Shelf
            {
                Id = tr.Id,
                Name = tr.Name,
                Type = tr.Type,
                BookCount = tr.BookCount,
                IsActived = tr.IsActived,

                Books = _context.Books.Where(w => tr.Id == w.ShelfId).Where(x => x.IsDeleted == false).Select(e => new Book
                {
                    Id = e.Id,
                    Name = e.Name,
                    Aurther = e.Aurther,
                    PDF = e.PDF,
                    Price = e.Price,
                    ShelfId = e.ShelfId,
                    Quantity = e.Quantity,
                    PDFFile = e.PDFFile,



                }
                            ).ToList()
            }).ToListAsync();
            return Shelfs;
        }
		public async Task<List<Shelf>> GetAllArchive()
		{
			var Shelfs = await _context.Shelfs.Where(x => x.IsDeleted == true).Select(tr => new Shelf
			{
				Id = tr.Id,
				Name = tr.Name,
				Type = tr.Type,
				BookCount = tr.BookCount,
				IsActived = tr.IsActived,

				Books = _context.Books.Where(w => tr.Id == w.ShelfId).Select(e => new Book
				{
					Id = e.Id,
					Name = e.Name,
					Aurther = e.Aurther,
					PDF = e.PDF,
					Price = e.Price,
					ShelfId = e.ShelfId,
					Quantity = e.Quantity,
					PDFFile = e.PDFFile,



				}
							).ToList()
			}).ToListAsync();
			return Shelfs;
		}

		public async Task<Shelf> Retrive(int Id)
		{
			var shelf = await GetShelf(Id);
			shelf.IsDeleted = false;
			_context.Entry(shelf).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return shelf;
		}
		public async Task<Shelf> DeletePermenetly(int Id)
		{
			var shelf = await GetShelf(Id);
			
			_context.Entry(shelf).State = EntityState.Deleted;
			await _context.SaveChangesAsync();
			return shelf;
		}
		public async Task<Shelf> GetShelf(int Id)
        {
            var singleShelf = await _context.Shelfs.Where(a => a.Id == Id).Select(tr => new Shelf
            {
                Id = tr.Id,
                Name = tr.Name,
                Type = tr.Type,
             
                BookCount = tr.BookCount,
                IsActived = tr.IsActived,

                Books = _context.Books.Where(w => tr.Id == w.ShelfId && w.IsDeleted == false).Select(e => new Book
                {
                    Id = e.Id,
                    Name = e.Name,
                    Aurther = e.Aurther,
                    PDF = e.PDF,
                    Price = e.Price,
                    ShelfId = e.ShelfId,
                    Quantity = e.Quantity,
                    PDFFile = e.PDFFile,



                }
                            ).ToList()
            }).FirstOrDefaultAsync();
            return singleShelf;
        }
        public async Task<Shelf> Edit(int Id, Shelf EditedShelf)
        {
            var shelf = await GetShelf(Id);
            EditedShelf.BookCount = shelf.BookCount;
            _context.Entry(EditedShelf).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return EditedShelf;
        }

        public async Task<Shelf> Delete(int Id)
        {
            var shelf = await GetShelf(Id);
            shelf.IsDeleted = true;
            _context.Entry(shelf).State = EntityState.Modified;
            await _context.SaveChangesAsync();
return shelf;
        }


    }
}
