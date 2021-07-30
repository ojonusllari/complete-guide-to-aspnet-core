using my_books.Data.Model;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class AuthorsService
    {
        private AppDbContext _context;

        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM author)
        {
            var _author = new Author()
            {
                FullName = author.FullName,
          
            };
            _context.Author.Add(_author);
            _context.SaveChanges();

        }   
        public AuthorWithBooksVm GetAuthorWithBooks(int authorId)
        {
            var _author = _context.Author.Where(n => n.Id == authorId).Select(book => new AuthorWithBooksVm()
            {
                FullName = book.FullName,
                BookTitles = book.Book_Author.Select(n => n.Book.Title).ToList()
            }).FirstOrDefault();

            return _author;
        }
    }
}
