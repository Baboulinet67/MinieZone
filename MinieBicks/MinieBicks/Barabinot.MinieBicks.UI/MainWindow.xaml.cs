using Barabinot.MiniBicks.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                           
                this.ListeBoxUser.ItemsSource = listUser;
                this.ListeBoxUser.DisplayMemberPath = "Nom";
            }
        }

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

        private void Button_Click(object sender, RoutedEventArgs e)
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int congePose = 0;
            int RttPose = 0;
            if (this.DebutConge.SelectedDate != null && this.FinConge.SelectedDate != null)
            {
                DateTime debutConge = (DateTime)this.DebutConge.SelectedDate;
                DateTime finConge = (DateTime)this.FinConge.SelectedDate;

                congePose = Math.Abs((finConge - debutConge).Days) + 1;
            }
            
            if(this.DebutRtt.SelectedDate != null && this.FinRtt.SelectedDate != null)
            {
                DateTime debutRtt = (DateTime)this.DebutRtt.SelectedDate;
                DateTime finRtt = (DateTime)this.FinRtt.SelectedDate;

                RttPose = Math.Abs((finRtt - debutRtt).Days) + 1;
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
                    db.SaveChanges();   
                }
                RefreshConge();
            }
        }

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
    }
}
