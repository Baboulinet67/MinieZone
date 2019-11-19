using System;

namespace Barabinot.MiniBicks.Lib
{
    public class Utilisateur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public int Role { get; set; }
        public int IdSuperieur { get; set; }
    }
}
