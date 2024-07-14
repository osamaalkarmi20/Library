using DataLayer.Models;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Interface
{
    public interface IBook
    { 
        public Task<Book> GetBook( int Id);
        public Task<List<Book>> GetAll();
        public Task<Book> Edit(int Id, Book EditedBook, IFormFile? formFile);
        public Task<Book> Create(Book book);
        public byte[] ConvertIFormFileToByteArray(IFormFile? formFile);
		public Task<Book> Retrive(int Id);
		public Task<Book> DeletePermenetly(int Id);
		public Task<List<Book>> GetAllArchive();

		public Task<Book> Delete(int Id);

    }
}
