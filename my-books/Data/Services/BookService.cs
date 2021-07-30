using my_books.Data.Model;
using my_books.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_books.Data.Services
{
    public class BookService
    {
        private AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBookWithAuthors(BookVm book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            _context.Books.Add(_book);
            _context.SaveChanges();


            var book_author = new List<Book_Author>();
            foreach (var id in book.AuthorIds)
            {
                book_author.Add(new Book_Author
                {
                    BookId = _book.Id,
                    AuthorId = id
                });
                // var _book_author = new Book_Author()
                //{
                //    BookId = _book.Id,
                //    AuthorId = id
                //};
            }
            _context.Book_Author.AddRange(book_author);
            _context.SaveChanges();

        }

        public List<Book> GetAllBooks() => _context.Books.ToList();

        //other way of geting data
        //public List<Book> GetAllBooks()
        //{
        //    return _context.Books.ToList();
        //}


        public BookWithAuthorsVm GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVm()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Author.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }
    

        public Book UpdateBookById(int BookId, BookVm book)
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == BookId);
            if(_book != null)
            {

                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.DateAdded = DateTime.Now;

                 _context.SaveChanges();
            };

            return _book;
        }

        public void DeleteBookById(int bookId) 
        {
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();

            }
        }
        

    }

}
