using Barabinot.MiniBicks.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        //Chargement de la liste utilisateur
        private void ChargerListeUser()
        {
            using (var db = new GestionContext())
            {
                // Read
                Console.WriteLine("Lecture des utilisateurs");
                var users = db.Utilisateurs
                    .OrderBy(b => b.Nom);

                var listUser = users.ToList();
                           
                this.ListeBoxUser.ItemsSource = listUser;
                this.ListeBoxUser.DisplayMemberPath = "Nom";
            }
        }

        //Action mise en place lorsque la selection de l'utilisateur a changée
        private void ListeBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.TextBoxNom.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Nom;
            this.TextBoxPrenom.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Prenom;
            this.TextBoxRue.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Rue;
            this.TextBoxCodePostal.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).CodePostal;
            this.TextBoxVille.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Ville;
            this.TextBoxPays.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Pays;
            this.TextBoxRole.Text = ((Utilisateurs)ListeBoxUser.SelectedItem).Role.ToString();

            using (var db = new GestionContext())
            {
                // affiche le compteur de congés
                Console.WriteLine("Lecture des congés de l'utilisateur");
                var user = db.Utilisateurs.Include(p => p.Conge).Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                this.LblConge.Content = user.Conge.NbConge;
                this.LblRtt.Content = user.Conge.NbRTT;
            }
        }

        //Modification de l'utilisateur selectionné
        private void Button_ModifUser(object sender, RoutedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                var user = db.Utilisateurs.Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();

                // Update
                Console.WriteLine("Mise à jour de l'utilisateur");
                user.Nom = this.TextBoxNom.Text;
                user.Prenom = this.TextBoxPrenom.Text;
                user.Rue = this.TextBoxRue.Text;
                user.CodePostal = this.TextBoxCodePostal.Text;
                user.Ville = this.TextBoxVille.Text;
                user.Pays = this.TextBoxPays.Text;
                user.Role = Convert.ToInt32(this.TextBoxRole.Text);
                db.SaveChanges();
            }
            ChargerListeUser();
        }

        //Validation d'un congé et/ou d'un RTT
        private void Button_ValidConge(object sender, RoutedEventArgs e)
        {
            int congePose = 0;
            int RttPose = 0;
            if (this.DebutConge.SelectedDate != null && this.FinConge.SelectedDate != null)
            {
                DateTime debutConge = (DateTime)this.DebutConge.SelectedDate;
                DateTime finConge = (DateTime)this.FinConge.SelectedDate;

                congePose = Utils.GetNumberOfWorkingDays(debutConge, finConge);
                //congePose = Math.Abs((finConge - debutConge).Days) + 1;
            }
            
            if(this.DebutRtt.SelectedDate != null && this.FinRtt.SelectedDate != null)
            {
                DateTime debutRtt = (DateTime)this.DebutRtt.SelectedDate;
                DateTime finRtt = (DateTime)this.FinRtt.SelectedDate;
                RttPose = Utils.GetNumberOfWorkingDays(debutRtt, finRtt);
                //RttPose = Math.Abs((finRtt - debutRtt).Days) + 1;
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

        //Permet de mettre à jour les valeurs affichées dans la page congés
        private void RefreshConge()
        {
            using (var db = new GestionContext())
            {
                // affiche le compteur de congés
                Console.WriteLine("Lecture des congés de l'utilisateur");
                var user = db.Utilisateurs.Include(p => p.Conge).Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();
                this.LblConge.Content = user.Conge.NbConge;
                this.LblRtt.Content = user.Conge.NbRTT;
            }
            this.DebutConge.SelectedDate = null;
            this.FinConge.SelectedDate = null;

            this.DebutRtt.SelectedDate = null;
            this.FinRtt.SelectedDate = null;
        }

        //Vérification de saisie
        private void TxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.TxtBoxTransport.Text = Utils.IsTextAllowed(this.TxtBoxTransport.Text);
            this.TxtBoxKm.Text = Utils.IsTextAllowed(this.TxtBoxKm.Text);
            this.TxtBoxParking.Text = Utils.IsTextAllowed(this.TxtBoxParking.Text);
            this.TxtBoxPhone.Text = Utils.IsTextAllowed(this.TxtBoxPhone.Text);
            this.TxtBoxRepas.Text = Utils.IsTextAllowed(this.TxtBoxRepas.Text);
            this.TxtBoxLogement.Text = Utils.IsTextAllowed(this.TxtBoxLogement.Text);
            this.TxtBoxDivers.Text = Utils.IsTextAllowed(this.TxtBoxDivers.Text);

            decimal totFrais = 0;
            decimal fraisTransport = Convert.ToDecimal(this.TxtBoxTransport.Text);
            //Les frais kilométriques sont remboursé à hauteur de 0.33€ par Km
            decimal fraisKm = Convert.ToDecimal(this.TxtBoxKm.Text) * 0.33m;
            decimal fraisParking = Convert.ToDecimal(this.TxtBoxParking.Text);
            decimal fraisPhone = Convert.ToDecimal(this.TxtBoxPhone.Text);
            decimal fraisRepas = Convert.ToDecimal(this.TxtBoxRepas.Text);
            decimal fraisLogement = Convert.ToDecimal(this.TxtBoxLogement.Text);
            decimal fraisDivers = Convert.ToDecimal(this.TxtBoxDivers.Text);
            totFrais = fraisTransport + fraisKm + fraisParking + fraisPhone + fraisRepas + fraisLogement + fraisDivers;

            this.lblTotFrais.Content = totFrais;
        }

        
        //Validation des frais de l'utilisateur
        private void Button_ValidFrais(object sender, RoutedEventArgs e)
        {
            using (var db = new GestionContext())
            {
                var user = db.Utilisateurs.Where(b => b.UserId == ((Utilisateurs)ListeBoxUser.SelectedItem).UserId).First();

                Frais frais = new Frais();
                decimal reportSuivant = 0;
                decimal fraisJour = Convert.ToDecimal(this.lblReport.Content);
                frais.DateFrais = (DateTime)this.DateFrais.SelectedDate;
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
