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

        public Utilisateur utilisateur { get; set; }



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

        public int validationCommande(string sexe)
        {
            if(this.ListeArticle.Count == 0)
            {
                throw new Exception("EmptyArticleException");
            }

            if(sexe == "M")
            {
                utilisateur.SexePersonne = Utilisateur.Sexe.Male;
                return 0;
            }
            else if (sexe == "F")
            {
                utilisateur.SexePersonne = Utilisateur.Sexe.Female;
                return 0;
            }
            else
            {
                throw new Exception("WrongSexException");
                return -1;
            }

        }

        public void calculFraisLivraison()
        {
            DictionnairePays dic = new DictionnairePays();
            
            if(this.ListeArticle.Count > 1)
            {
                dic.LivraisonPays.TryGetValue(this.Adresse.Pays, out int index);
                
                if (index == 1)
                {
                    this.FraisLivraison = 5;
                    if (this.ListeArticle.Count > 3)
                    {
                        this.FraisLivraison = 0;
                    }
                }
                else if(index == 2)
                {
                    this.FraisLivraison = 10;
                    if(this.getSommeTtc() > 50 || this.ListeArticle.Count > 4)
                    {
                        this.FraisLivraison = 0;
                    }
                }
                else
                {
                    this.FraisLivraison = 20;
                    if(this.ListeArticle.Count > 5 && this.getSommeTtc() > 100)
                    {
                        this.FraisLivraison = 0;
                    }
                }
            }
            else
            {
                this.FraisLivraison = 0;
            }
        }

        public decimal calculPrixMoyen()
        {
            if(this.ListeArticle.Count > 0)
            {
                return (this.getSommeTtc() / this.ListeArticle.Count);
            }
            else
            {
                return 0;
            }
        }

    }

    public class OrderCompleted : EventArgs
    {
        public int Threshold { get; set; }
        public DateTime TimeReached { get; set; }
    }
}
