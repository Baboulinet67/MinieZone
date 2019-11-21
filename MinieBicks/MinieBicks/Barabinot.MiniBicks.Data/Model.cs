using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Barabinot.MiniBicks.Data
{
    public class GestionContext : DbContext
    {
        public DbSet<Utilisateurs> Utilisateurs { get; set; }
        public DbSet<Conge> Conge { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=D:\\Documents\\CESI\\MinieZone\\MinieBicks\\MinieBicks\\Barabinot.MiniBicks.Data\\gestionnaire.db");
    }

    public class Utilisateurs
    {
        [Key]
        public int UserId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Rue { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Pays { get; set; }
        public int Role { get; set; }
        public int IdSuperieur { get; set; }
        public Conge Conge { get; set; }
        public List<Frais> Frais { get; } = new List<Frais>();
    }

    public class Conge
    {
        public int CongeId { get; set; }
        public int NbConge { get; set; }
        public int NbRTT { get; set; }
    }

    public class Frais
    {
        public int FraisId { get; set; }
        public DateTime DateFrais { get; set; }
        public decimal TotFrais { get; set; }
        public decimal ReportFrais { get; set; }
    }
}
