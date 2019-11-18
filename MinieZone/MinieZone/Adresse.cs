using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class Adresse
    {
        public string Rue { get; set; }
        public int CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }


        public Adresse(string rue, int codePostal, string ville, string pays)
        {
            Rue = rue;
            CodePostal = codePostal;
            Ville = ville;
            Pays = pays;
        }

    }
}
