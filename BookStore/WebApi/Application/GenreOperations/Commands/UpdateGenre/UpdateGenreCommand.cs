using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public UpdateGenreModel Model { get; set; }
        public int GenreId { get; set; }

        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var genre=_context.Genres.SingleOrDefault(x=>x.Id==GenreId);
            if(genre is null){
                throw new InvalidOperationException("Kitap turu bulunamadi");
            }


            if(_context.Genres.Any(x=>x.Name.ToLower()==Model.Name.ToLower() && x.Id!=GenreId)){
                throw new InvalidOperationException("Ayni isimle bir kitap turu zaten mevcut");
            }

            genre.Name=string.IsNullOrEmpty(Model.Name.Trim())?genre.Name:Model.Name;
            genre.IsActive=Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}