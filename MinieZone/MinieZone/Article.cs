using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class Article
    {
        public Guid id { get; set; } = Guid.NewGuid();

        public string Nom { get; set; } = "";

        private decimal _prixHt;
        public decimal PrixHt {
            get
            {
                return this._prixHt;
            } 
            set 
            { 
                this._prixHt = Math.Abs(value);
            } 
        }

        public decimal TauxTva { get; set; }

        public Article(string nom, decimal prixHt, decimal tauxTva)
        {
            Nom = nom;
            PrixHt = prixHt;
            TauxTva = tauxTva;
        }
    }
}
