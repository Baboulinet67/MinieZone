using MinieZoneLibrary;
using System;
using System.Collections.Generic;

namespace MinieZone
{
    public class Commande
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public List<Article> ListeArticle { get; set; }

        public decimal mntHt { get; set; }

        public decimal tauxTVA { get; set; }

        public decimal mntTtc { get; set; }

        public decimal mntLivraison { get; set; }

        //public decimal somme { get; }

        Commande()
        {

        }
    }
}
