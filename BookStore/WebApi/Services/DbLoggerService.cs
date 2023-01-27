namespace WebApi.Services{
    public class DbLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[DB Logger]"+message);
        }
    }
}