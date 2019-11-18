using MinieZoneLibrary;
using System;
using System.Collections.Generic;

namespace MinieZone
{
    public class Commande
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public DateTime date { get; set; } = DateTime.Now;
        public List<Article> ListeArticle { get; set; } = new List<Article>();

        //public decimal somme { get; }

        public decimal getSommeHt()
        {
            decimal somme = 0;
            foreach (var article in this.ListeArticle)
            {
                somme += article.PrixHt;
            }
            return somme;
        }

        public decimal getSommeTtc()
        {
            decimal somme = 0;
            foreach (var article in this.ListeArticle)
            {
                somme += (article.PrixHt * article.TauxTva);
            }
            return somme;
        }


    }
}
