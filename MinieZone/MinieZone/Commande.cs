using MinieZoneLibrary;
using System;
using System.Collections.Generic;

namespace MinieZoneLibrary
{
    public class Commande
    {
        public Commande(Adresse adresse, Adresse adresseFacturation)
        {
            Adresse = adresse;
            AdresseFacturation = adresseFacturation;
        }

        public Commande(Adresse adresse)
        {
            Adresse = adresse;
            AdresseFacturation = adresse;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCommande { get; set; } = DateTime.Now;
        public List<Article> ListeArticle { get; set; } = new List<Article>();
        public Adresse Adresse { get; set; }
        public Adresse AdresseFacturation { get; set; }

        public DateTime DateLivraison { 
            get => DateCommande.AddDays(7);
        }

        public int FraisLivraison { get; private set; }


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

        public void validationCommande()
        {

        }

        public void calculFraisLivraison()
        {
            DictionnairePays dic = new DictionnairePays();
            
            if(this.ListeArticle.Count > 1 && this.ListeArticle.Count < 4)
            {
                dic.LivraisonPays.TryGetValue(this.Adresse.Pays, out int index);
                
                if (index == 1)
                {
                    this.FraisLivraison = 5;
                }
                else if(index == 2)
                {
                    this.FraisLivraison = 10;
                }
                else
                {
                    this.FraisLivraison = 20;
                }
            }
            else
            {
                this.FraisLivraison = 0;
            }
        }

        public decimal calculPrixMoyen()
        {
            return (this.getSommeTtc() / this.ListeArticle.Count);
        }
    }
}
