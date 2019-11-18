using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class Article
    {
        public Guid id { get; set; } = Guid.NewGuid();

        public string Nom { get; set; } = "";

        public decimal PrixHt { get; set; } = 0;

        public decimal TauxTva { get; set; } = 0;

        public Article(string nom, decimal prixHt, decimal tauxTva)
        {
            Nom = nom;
            PrixHt = prixHt;
            TauxTva = tauxTva;
        }
    }
}
