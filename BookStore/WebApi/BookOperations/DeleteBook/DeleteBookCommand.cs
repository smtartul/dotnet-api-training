using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBookCommand{
    public class DeleteBookCommand{
        private readonly BookStoreDbContext _dbContext;
        public int BookId { get; set; }

        public DeleteBookCommand(BookStoreDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public void Handle(){
             var book = _dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Kayit bulunamadi");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
        }
    }
}