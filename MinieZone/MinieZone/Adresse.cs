using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class Adresse
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Rue { get; set; }
        public int CodePostal { get; set; }
        public string Ville { get; set; }

        public Adresse(string nom, string prenom, string rue, int codePostal, string ville)
        {
            Nom = nom;
            Prenom = prenom;
            Rue = rue;
            CodePostal = codePostal;
            Ville = ville;
        }


    }
}
