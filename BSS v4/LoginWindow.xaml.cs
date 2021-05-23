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
using System.IO;

namespace BSS_v4
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        #region MEMEBER VARIABELEN
        private int _wachtwoordPogingenTeller = 3;
        private Dictionary<string, string> geregistreerdeSpelers = new Dictionary<string, string>();
        #endregion
        public LoginWindow()
        {
            InitializeComponent();
            LaadGebruikersIn();
            CboSpelers.SelectedIndex = 0;
        }

        #region BUTTONS
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (PwdLogin.Password.Equals("") || PwdLogin.Password.Equals("Wachtwoord"))
            {
                MessageBox.Show($"Gelieve een wachtwoord in te geven", "Geen wachtwoord", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                LoginGebruiker();
            }
            
        }

        private void BtnRegistreer_Click(object sender, RoutedEventArgs e)
        {
            RegistreerGebruiker(TxtSpelerRegistratie.Text, PwdWachtwoordRegistratie.Password.ToString(), PwdWachtwoordRegistratieValidatie.Password.ToString());
        }

        #endregion

        #region GEBRUIKER LOGIN EN REGISTRATIE
        private void LoginGebruiker()
        {
            string geselecteerdeSpeler = CboSpelers.SelectedItem.ToString();

            foreach (var speler in geregistreerdeSpelers)
            {
                if (Equals(geselecteerdeSpeler, speler.Value) && (Equals(PwdLogin.Password, speler.Key)))
                {
                    MainWindow spelScherm = new MainWindow(geselecteerdeSpeler);
                    this.Close();
                    spelScherm.ShowDialog();
                }
                else if (Equals(geselecteerdeSpeler, speler.Value) && (!Equals(PwdLogin.Password, speler.Key)))
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
                        PwdLogin.Clear();
                    }
                }
            }
        }

        // Validatie voor correct wachtwoord
        // Reset alle velden terug naar hun default waarde en toont succesmelding
        // Roept methode op om de gebruiker aan Users.txt toe te voegen
        private void RegistreerGebruiker(string gebruikersnaam, string wachtwoord, string wachtwoordValidatie)
        {
            string speler = gebruikersnaam;

            if (wachtwoord.Equals(wachtwoordValidatie))
            {
                geregistreerdeSpelers.Add(wachtwoord, speler);
                UpdateCboSpelers(speler);
                SchrijfGeregistreerdeGebruikerWeg(wachtwoord, speler);

                TxtSpelerRegistratie.Text = "Gebruikersnaam";
                TxtSpelerRegistratie.Opacity = 0.5;
                PwdWachtwoordRegistratie.Password = "Wachtwoord";
                PwdWachtwoordRegistratie.Opacity = 0.5;
                PwdWachtwoordRegistratieValidatie.Password = "Wachtwoord";
                PwdWachtwoordRegistratieValidatie.Opacity = 0.5;
                LblMeldingRegistratie.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Beide wachtwoorden moeten hetzelfde zijn.", "Wachtwoord foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region GEBRUIKERS INLADEN EN WEGSCHRIJVEN

        // Gebruikers worden ingeladen en aan geregistreerdeSpelers List en spelers Combobox toegevoegd
        private void LaadGebruikersIn()
        {
            StreamReader reader;
            reader = File.OpenText("files/Users.txt");
            string[] ingelezenVelden;

            while (!reader.EndOfStream)
            {
                ingelezenVelden = reader.ReadLine().Split(',');
                geregistreerdeSpelers.Add(ingelezenVelden[0], ingelezenVelden[1]);
                CboSpelers.Items.Add(ingelezenVelden[1]);
            }

            reader.Close();
        }

        // Voegt de geregistreerde speler toe aan Users.txt
        private void SchrijfGeregistreerdeGebruikerWeg(string wachtwoord, string gebruikersnaam)
        {
            FileStream stream = new FileStream("files/Users.txt", FileMode.Append, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            StringBuilder nieuweGebruiker = new StringBuilder();
            nieuweGebruiker.Append(Environment.NewLine);
            nieuweGebruiker.Append(wachtwoord);
            nieuweGebruiker.Append(",");
            nieuweGebruiker.Append(gebruikersnaam);

            writer.Write(nieuweGebruiker.ToString());

            writer.Close();
            stream.Close();

            nieuweGebruiker.Clear();
        }

        private void UpdateCboSpelers(string speler)
        {
            CboSpelers.Items.Add(speler);
        }
        #endregion

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

        private void PwdBoxLogin_GotFocus(object sender, RoutedEventArgs e)
        {

            PwdLogin.Clear();
            PwdLogin.Opacity = 1;
        }

        private void TxtSpelerRegistratie_GotFocus(object sender, RoutedEventArgs e)
        {
            if (TxtSpelerRegistratie.Text == "Gebruikersnaam")
            {
                TxtSpelerRegistratie.Clear();
                TxtSpelerRegistratie.Opacity = 1;
            }
            if(LblMeldingRegistratie.Visibility == Visibility.Visible)
            {
                LblMeldingRegistratie.Visibility = Visibility.Hidden;
            }
        }


        private void PwdWachtwoordRegistratie_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PwdWachtwoordRegistratie.Password == "Wachtwoord")
            {
                PwdWachtwoordRegistratie.Clear();
                PwdWachtwoordRegistratie.Opacity = 1;
            }
        }

        private void PwdWachtwoordRegistratieValidatie_GotFocus(object sender, RoutedEventArgs e)
        {
            if (PwdWachtwoordRegistratieValidatie.Password == "Wachtwoord")
            {                           
                PwdWachtwoordRegistratieValidatie.Clear();
                PwdWachtwoordRegistratieValidatie.Opacity = 1;
            }
        }
        #endregion
    }
}
