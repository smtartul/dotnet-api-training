namespace WebApi.Services{
    public class ConsoleLoggerService : ILoggerService
    {
        public void Log(string message)
        {
            Console.WriteLine("[Console Logger]"+message);
        }
    }
}