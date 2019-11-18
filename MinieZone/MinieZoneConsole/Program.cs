using MinieZoneLibrary;
using System;
using System.IO;

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

            Commande cmd2 = new Commande(adr);
            cmd2.addArticle(article1);

            try
            {
                Console.WriteLine("Pour valider la commande, merci de confirmer votre sexe : M(ale) F(emele) ");
                string sexe = Console.ReadLine();
                if(cmd2.validationCommande(sexe) == 0)
                {
                    Console.WriteLine("Commande Validée");
                }
                else
                {
                    Console.WriteLine("La commande n'a pas pu être validée");
                }
            }
            catch (Exception e)
            {
                if (e.Message == "EmptyArticleException")
                {
                    Console.WriteLine("Une commande ne peux pas être validée avec 0 articles");
                    log(e);
                }
                if (e.Message == "WrongSexException")
                {
                    Console.WriteLine("Le sexe n'a pas été correctement renseigné");
                    log(e);
                }
            }

            Console.ReadKey();
        }

        public static void log(Exception e)
        {
            StreamWriter sw = File.AppendText("D:\\Documents\\CESI\\MinieZone\\MinieZone\\MinieZoneConsole\\log.txt");
            
            sw.WriteLine(DateTime.Now +" "+ e.ToString());

            sw.Close();
        }
    }
}
