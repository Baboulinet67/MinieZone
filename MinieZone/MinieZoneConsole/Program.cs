using MinieZoneLibrary;
using System;

namespace MinieZoneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Article article1 = new Article("Article 1", (decimal)5, (decimal)0.2);
            Article article2 = new Article("Article 2", (decimal)10, (decimal)0.2);
            Article article3 = new Article("Article 3", (decimal)50, (decimal)0.2);


        }
    }
}
