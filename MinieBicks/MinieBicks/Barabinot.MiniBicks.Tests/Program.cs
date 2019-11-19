using Barabinot.MiniBicks.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Barabinot.MiniBicks.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new GestionContext())
            {
                // Read
                Console.WriteLine("Lecture des utilisateurs");
                var users = db.Utilisateurs
                    .OrderBy(b => b.Nom);
                
                var listUser = users.ToList();
                foreach (var user in listUser)
                {
                    Console.WriteLine(user.Nom + " " + user.Prenom);
                }

            }
        }
    }
}
