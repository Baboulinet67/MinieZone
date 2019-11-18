using MinieZoneLibrary;
using System;

namespace MinieZoneConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            testGlobal();
        }

        public static void testDecimal()
        {
            Console.WriteLine("Somme decimal = " + (0.1m + 0.1m + 0.1m + 0.1m + 0.1m + 0.1m + 0.1m + 0.1m + 0.1m + 0.1m));
            Console.WriteLine("Multiplication decimal = " + (0.1m *10));

            Console.WriteLine("Somme double = " + (0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1 + 0.1));
            Console.WriteLine("Multiplication double = " + (0.1 * 10));
        }

        public static void testGlobal()
        {
            Article article1 = new Article("Article 1", (decimal)5, (decimal)0.2);
            Article article2 = new Article("Article 2", (decimal)10, (decimal)0.2);
            Article article3 = new Article("Article 3", (decimal)50, (decimal)0.2);

            Adresse adr = new Adresse("rue", 67120, "Molsheim", "FRANCE");

            Commande commande1 = new Commande(adr);
            commande1.addArticle(article1);
            commande1.addArticle(article2);
            commande1.addArticle(article1);
            commande1.addArticle(article3);

            foreach (string article in commande1.affichagePanier())
            {
                Console.WriteLine(article);
                commande1.calculFraisLivraison();
                Console.WriteLine(commande1.FraisLivraison);
            }

            Console.WriteLine("Total Ht  : " + commande1.getSommeHt());
            Console.WriteLine("Total Ttc : " + commande1.getSommeTtc());

            Console.ReadKey();
        }
    }
}
