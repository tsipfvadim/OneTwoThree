namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var gameOperations = new GameOperations();

            for (var i = 0; i < 100; i++)
                gameOperations.Iterate();
        }
    }
}