using MinieZoneLibrary;
using System;

namespace MinieZoneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Article article1 = new Article("Article 1", (decimal)5, (decimal)0.2);
            Article article2 = new Article("Article 2", (decimal)10, (decimal)0.2);
            Article article3 = new Article("Article 3", (decimal)50, (decimal)0.2);

            Commande commande1 = new Commande();
            commande1.addArticle(article1);
            commande1.addArticle(article2);
            commande1.addArticle(article1);
            commande1.addArticle(article3);

            foreach(string article in commande1.affichagePanier())
            {
                Console.WriteLine(article);
            }

            Console.WriteLine("Total Ht  : " + commande1.getSommeHt());
            Console.WriteLine("Total Ttc : " + commande1.getSommeTtc());

            Console.ReadKey();
        }
    }
}
