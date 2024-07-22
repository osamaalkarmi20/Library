
using Hangfire;
using ServiceLayer.Interface;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Library
{
    public  class Job
    {
        private readonly LibraryDbContext _context;
        public Job(LibraryDbContext context)
        {
            _context= context;
        }
        public async Task  Booklog()
        {
        var count =   await _context.Books.CountAsync();
       
            Console.WriteLine($"this is the current book count {count} ");
        }

    }
}
