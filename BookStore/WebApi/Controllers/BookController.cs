using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.BookOperations.CreateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                // if (!result.IsValid)
                // {
                //     foreach (var res in result.Errors)
                //     {
                //         Console.WriteLine("Ozellik " + res.PropertyName + "- Error Message = " + res.ErrorMessage);
                //     }
                // }
                // else
                // {
                //     command.Handle();

                // }

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
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.BookId = id;
                command.Model = updatedBook;
                UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
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
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.BookId = id;
                DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

    }
}