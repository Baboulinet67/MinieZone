using System;
using System.Collections.Generic;
using System.Text;

namespace MinieZoneLibrary
{
    public class Utilisateur
    {
        public enum Sexe { Male, Female };

        private Sexe _sexe;

        public Sexe SexePersonne
        {
            get { return _sexe; }
            set { _sexe = value; }
        }
    }
}
