using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.Common.CreateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookQuery;
using static WebApi.Common.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id=1,
        //         Title="Learn Startup",
        //         GenreId=1,// category Personal Growth
        //         PageCount=200,
        //         PublishDate=new DateTime(2001,06,12)
        //     },
        //      new Book{
        //         Id=2,
        //         Title="Herland",
        //         GenreId=2,// category Science
        //         PageCount=160,
        //         PublishDate=new DateTime(2010,05,23)
        //     },
        //      new Book{
        //         Id=3,
        //         Title="Dune",
        //         GenreId=2,
        //         PageCount=540,
        //         PublishDate=new DateTime(2001,12,21)
        //     }
        // };

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context);
                query.BookId = id;
                result = query.Handle();
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
            return Ok(result);

        }

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            UpdateBookQuery query = new UpdateBookQuery(_context);
            try
            {
                query.BookId=id;
                query.Model = updatedBook;
                query.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }

    }
}