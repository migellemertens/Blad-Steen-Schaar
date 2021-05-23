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
using System.Windows.Shapes;

namespace BSS_v3
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private int _wachtwoordPogingenTeller = 3;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            foreach (var speler in Wachtwoorden.geregistreerdeSpelers)
            {
                if (Equals(TxtSpeler.Text, speler.Value) && (Equals(PwdBoxLogin.Password, speler.Key)))
                {
                    MainWindow spelScherm = new MainWindow(TxtSpeler.Text);
                    this.Close();
                    spelScherm.ShowDialog();
                }
                else if (Equals(TxtSpeler.Text, speler.Value) && (!Equals(PwdBoxLogin.Password, speler.Key)))
                {
                    _wachtwoordPogingenTeller--;
                    if (_wachtwoordPogingenTeller == 0)
                    {
                        MessageBox.Show($"U heeft geen pogingen meer over. Applicatie wordt gesloten", "Geen pogingen over", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show($"U heeft nog {_wachtwoordPogingenTeller} pogingen over", "Fout wachtwoord", MessageBoxButton.OK, MessageBoxImage.Warning);
                        PwdBoxLogin.Clear();
                    }
                }
            }
        }

        #region TITLE BAR ACTIES

        // De title bar + minimaliseren-maximaliseren-sluiten knop zijn apart gemaakt om zo
        // beter te passen bij de layout. Onderstaande acties zorgen voor de functionaliteit.
        private void BtnSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                this.BorderThickness = new Thickness(0);
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                this.BorderThickness = new Thickness(6);
            }
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        #endregion

        #region VELDEN LEEGMAKEN BIJ FOCUS
        private void TxtSpeler_GotFocus(object sender, RoutedEventArgs e)
        {
            if(TxtSpeler.Text == "Gebruikersnaam")
            {
                TxtSpeler.Clear();
                TxtSpeler.Opacity = 1;
            }
        }

        private void PwdBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {
            
                PwdBoxLogin.Clear();
                TxtSpeler.Opacity = 1;
        }
        #endregion
    }
}
