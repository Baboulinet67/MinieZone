using MinieZoneLibrary;
using System;
using System.Collections.Generic;

namespace MinieZoneLibrary
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
                somme += (article.PrixHt * (article.TauxTva + 1));
            }
            return somme;
        }

        public void addArticle(Article art)
        {
            this.ListeArticle.Add(art);
        }

        public List<string> affichagePanier()
        {
            List<string> panier = new List<string>();

            foreach(var article in this.ListeArticle)
            {
                panier.Add("Nom : " + article.Nom + " Prix hors taxe : " + article.PrixHt + " Taux TVA : " + article.TauxTva);
            }

            return panier;
        }
    }
}
