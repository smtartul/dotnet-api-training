using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var genre=_context.Genres.SingleOrDefault(command=>command.Id==GenreId);
            if(genre is null){
                throw new InvalidOperationException("Kitap turu bulunamadi");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}