using Barabinot.MiniBicks.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Barabinot.MiniBicks.Lib;

namespace Barabinot.MinieBicks.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //On commence par charger tous les utilisateurs de la table Utilisateurs
            ChargerListeUser();
        }


        private void ChargerListeUser()
        {
            using (var db = new GestionContext())
            {
                // Read
                Console.WriteLine("Lecture des utilisateurs");
                var users = db.Utilisateurs
                    .OrderBy(b => b.Nom);

                var listUser = users.ToList();
                           
                ListeBoxUser.ItemsSource = listUser;
                ListeBoxUser.DisplayMemberPath = "Nom";
            }
        }

        private void ListeBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxNom.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Nom;
            TextBoxPrenom.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Prenom;
            TextBoxRue.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Rue;
            TextBoxCodePostal.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).CodePostal;
            TextBoxVille.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Ville;
            TextBoxPays.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Pays;
            TextBoxRole.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Role.ToString();

            using (var db = new GestionContext())
            {
                // affiche le compteur de congés
                Console.WriteLine("Lecture des congés de l'utilisateur");
                var user = db.Utilisateurs.Include(p => p.Conge).Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                LblConge.Content = user.Conge.NbConge;
                LblRtt.Content = user.Conge.NbRTT;
            }
        }


        private void Button_ModifUser(object sender, RoutedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                var user = db.Utilisateurs.Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();

                // Update
                Console.WriteLine("Mise à jour de l'utilisateur");
                user.Nom = TextBoxNom.Text;
                user.Prenom = TextBoxPrenom.Text;
                user.Rue = TextBoxRue.Text;
                user.CodePostal = TextBoxCodePostal.Text;
                user.Ville = TextBoxVille.Text;
                user.Pays = TextBoxPays.Text;
                user.Role = Convert.ToInt32(TextBoxRole.Text);
                db.SaveChanges();
            }
            ChargerListeUser();
        }

        
        private void Button_ValidConge(object sender, RoutedEventArgs e)
        {
            int congePose = 0;
            int RttPose = 0;
            if (DebutConge.SelectedDate != null && FinConge.SelectedDate != null)
            {
                DateTime debutConge = (DateTime)DebutConge.SelectedDate;
                DateTime finConge = (DateTime)FinConge.SelectedDate;

                congePose = Utils.GetNumberOfWorkingDays(debutConge, finConge);
                //congePose = Math.Abs((finConge - debutConge).Days) + 1;
            }
            
            if(DebutRtt.SelectedDate != null && FinRtt.SelectedDate != null)
            {
                DateTime debutRtt = (DateTime)DebutRtt.SelectedDate;
                DateTime finRtt = (DateTime)FinRtt.SelectedDate;
                RttPose = Utils.GetNumberOfWorkingDays(debutRtt, finRtt);
            }
            
            if (congePose != 0 || RttPose != 0)
            {
                using (var db = new GestionContext())
                {
                    // affiche le compteur de congés
                    Console.WriteLine("Lecture des congés de l'utilisateur");
                    var user = db.Utilisateurs.Include(p => p.Conge).Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                    user.Conge.NbConge -= congePose;
                    user.Conge.NbRTT -= RttPose;
                    if(user.Conge.NbConge < 0 || user.Conge.NbRTT < 0)
                    {
                        MessageBox.Show("Solde de congés insuffisant");
                    }
                    else
                    {
                        db.SaveChanges(); 
                    }
                }
            }
            RefreshConge();
        }

        
        private void RefreshConge()
        {
            using (var db = new GestionContext())
            {
                // affiche le compteur de congés
                Console.WriteLine("Lecture des congés de l'utilisateur");
                var user = db.Utilisateurs.Include(p => p.Conge).Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                LblConge.Content = user.Conge.NbConge;
                LblRtt.Content = user.Conge.NbRTT;
            }
            DebutConge.SelectedDate = null;
            FinConge.SelectedDate = null;

            DebutRtt.SelectedDate = null;
            FinRtt.SelectedDate = null;
        }

        //Vérification de saisie
        private void TxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            TxtBoxTransport.Text = Utils.IsTextAllowed(TxtBoxTransport.Text);
            TxtBoxKm.Text = Utils.IsTextAllowed(TxtBoxKm.Text);
            TxtBoxParking.Text = Utils.IsTextAllowed(TxtBoxParking.Text);
            TxtBoxPhone.Text = Utils.IsTextAllowed(TxtBoxPhone.Text);
            TxtBoxRepas.Text = Utils.IsTextAllowed(TxtBoxRepas.Text);
            TxtBoxLogement.Text = Utils.IsTextAllowed(TxtBoxLogement.Text);
            TxtBoxDivers.Text = Utils.IsTextAllowed(TxtBoxDivers.Text);

            decimal totFrais = 0;
            decimal fraisJour = Convert.ToDecimal(this.lblReport.Content);
            decimal fraisTransport = Convert.ToDecimal(TxtBoxTransport.Text);
            //Les frais kilométriques sont remboursé à hauteur de 0.33€ par Km
            decimal fraisKm = Convert.ToDecimal(TxtBoxKm.Text) * 0.33m;
            decimal fraisParking = Convert.ToDecimal(TxtBoxParking.Text);
            decimal fraisPhone = Convert.ToDecimal(TxtBoxPhone.Text);
            decimal fraisRepas = Convert.ToDecimal(TxtBoxRepas.Text);
            decimal fraisLogement = Convert.ToDecimal(TxtBoxLogement.Text);
            decimal fraisDivers = Convert.ToDecimal(TxtBoxDivers.Text);
            totFrais = fraisTransport + fraisKm + fraisJour + fraisParking + fraisPhone + fraisRepas + fraisLogement + fraisDivers;

            this.lblTotFrais.Content = totFrais;
        }

        
        private void Button_ValidFrais(object sender, RoutedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                var user = db.Utilisateurs.Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();

                Frais frais = new Frais();
                frais.DateFrais = (DateTime)this.DateFrais.SelectedDate;
                decimal reportSuivant = 0;
                decimal fraisJour = Convert.ToDecimal(this.lblReport.Content);
                decimal fraisTransport = Convert.ToDecimal(this.TxtBoxTransport.Text);
                decimal fraisKm = Convert.ToDecimal(this.TxtBoxKm.Text) * 0.33m;
                decimal fraisParking = Convert.ToDecimal(this.TxtBoxParking.Text);
                decimal fraisPhone = Convert.ToDecimal(this.TxtBoxPhone.Text);
                decimal fraisRepas = Convert.ToDecimal(this.TxtBoxRepas.Text);
                decimal fraisLogement = Convert.ToDecimal(this.TxtBoxLogement.Text);
                decimal fraisDivers = Convert.ToDecimal(this.TxtBoxDivers.Text);

                if(fraisTransport > 27)
                {
                    reportSuivant = Math.Abs(27 - fraisTransport);
                    fraisTransport = 27;
                }

                frais.TotFrais = fraisTransport + fraisKm + fraisJour + fraisParking + fraisPhone + fraisRepas + fraisLogement + fraisDivers;

                user.Frais.Add(frais);
                db.SaveChanges();

                if(reportSuivant != 0)
                {
                    Frais fraisReport = new Frais();
                    fraisReport.DateFrais = ((DateTime)this.DateFrais.SelectedDate).AddDays(1);
                    fraisReport.ReportFrais = reportSuivant;
                    user.Frais.Add(fraisReport);
                    db.SaveChanges();
                }
            }
        }

        
        private void Button_AddUser(object sender, RoutedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                Utilisateurs user = new Utilisateurs();
                // Création d'un utilisateur
                Console.WriteLine("Création d'un utilisateur");
                user.Nom = this.TextBoxNom.Text;
                user.Prenom = this.TextBoxPrenom.Text;
                user.Rue = this.TextBoxRue.Text;
                user.CodePostal = this.TextBoxCodePostal.Text;
                user.Ville = this.TextBoxVille.Text;
                user.Pays = this.TextBoxPays.Text;
                user.Role = Convert.ToInt32(this.TextBoxRole.Text);

                Conge conge = new Conge();

                //Attribue le nombre de jour de congés et de RTT selon le pays de résidence de l'utilisateur
                switch (user.Pays.ToUpper().ToString())
                {
                    case "FRANCE":
                        conge.NbConge = 25;
                        conge.NbRTT = 10;
                        break;
                    case "BELGIQUE":
                        conge.NbConge = 25;
                        conge.NbRTT = 12;
                        break;
                    default:
                        conge.NbConge = 30;
                        conge.NbRTT = 0;
                        break;
                }

                user.Conge = conge;

                db.Add(user);
                db.SaveChanges();
            }
            ChargerListeUser();
        }

        private void ChargementReport(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                var user = db.Utilisateurs.Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                foreach(var frais in user.Frais)
                {
                    if(frais.DateFrais == (DateTime)this.DateFrais.SelectedDate)
                    {
                        this.lblReport.Content = frais.ReportFrais;
                    }
                }
            }

        }
    }
}
