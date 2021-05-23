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
using System.Windows.Threading;

namespace BSS_v3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region MEMBER VARIABELEN
        private DispatcherTimer _tijdTonenTimer = new DispatcherTimer();
        private DispatcherTimer _pauzeNaTeLaatTimer = new DispatcherTimer();
        private DispatcherTimer _resterendeTijdTimer;
        private Keuze _keuzeSpeler;
        private Keuze _keuzeComputer;
        private int _scoreSpeler;
        private int _scoreComputer;
        private int _resterendeSeconden;
        private string _spelerNaam;
        private int _pauze = 3;
        private bool _spelGestart;
        #endregion

        #region WINDOW CONSTRUCTOR
        public MainWindow()
        {
            InitializeComponent();
            NieuwSpel();
        }

        public MainWindow(string speler)
        {
            InitializeComponent();

            _spelerNaam = speler;
            NieuwSpel();

            _tijdTonenTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _tijdTonenTimer.Tick += timer_tick;
            _tijdTonenTimer.Start();

            StartSpelTimer();

            _pauzeNaTeLaatTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _pauzeNaTeLaatTimer.Tick += UpdatePauzeNaTeLaatTimer;
        }
        #endregion

        #region TIMERS
        private void timer_tick(object sender, EventArgs e)
        {
            LblToonTijd.Content = DateTime.Now.ToString("d MMMM yyyy HH:mm:ss");
        }

        private void StartSpelTimer()
        {
            _resterendeTijdTimer = new DispatcherTimer();
            _resterendeSeconden = 3;
            LblResterendeSeconden.Content = _resterendeSeconden;
            _resterendeTijdTimer.Interval = TimeSpan.FromMilliseconds(1000);
            _resterendeTijdTimer.Tick += UpdateResterendeTijd;
        }

        private void UpdateTimer()
        {
            _resterendeSeconden = 3;
            LblResterendeSeconden.Content = _resterendeSeconden;
        }

        private void UpdatePauzeNaTeLaatTimer(object sender, EventArgs e)
        {
            _pauze--;

            // Als de 3 seconden pauze voorbij zijn wordt de _pauzeNaTeLaatTimer gestopt en _pauze terug op 3 gezet
            // Melding verdwijnt en de reguliere speltimer wordt terug gereset en gestart
            if (_pauze == 0)
            {
                _pauzeNaTeLaatTimer.Stop();
                _pauze = 3;
                LblMelding.Content = "";
                _resterendeSeconden = 3;
                LblResterendeSeconden.Content = _resterendeSeconden;
                _resterendeTijdTimer.Start();
            }
        }

        // Tick event voor de resterende tijd - Update het label dat de resterende seconden toont
        private void UpdateResterendeTijd(object sender, EventArgs e)
        {
            _resterendeSeconden--;

            // Indien speler te laat is wordt de timer gestopt en krijgt de speler een melding te zien dat hij te laat was
            // De _pauzeNaTeLaatTimer wordt gestart zodat de speler 3 seconden heeft om het label te lezen en klaar is voor de volgende ronde
            if (_resterendeSeconden == 0)
            {
                _resterendeTijdTimer.Stop();

                LblMelding.Content = "U was te laat. De PC wint deze ronde";
                ImgKeuzeSpeler.Source = new BitmapImage(new Uri($"img/klok.png", UriKind.RelativeOrAbsolute));
                ImgKeuzeComputer.Source = new BitmapImage(new Uri($"img/klok.png", UriKind.RelativeOrAbsolute));

                _scoreComputer++;
                LblComputerScore.Content = _scoreComputer;

                UpdateBorder(new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Green));

                _pauzeNaTeLaatTimer.Start();
            }

            LblResterendeSeconden.Content = _resterendeSeconden;
        }
        #endregion

        #region BUTTON KEUZES
        // Comments gelden voor de 3 buttons
        // Button zet keuze adhv enumtype KEUZE
        // Toont spelerkeuze adhv een afbeelding en gaat na wie wint
        private void BtnSteen_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.STEEN;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            
            // Gaat na _spelGestart op false staat om de speltimer te starten
            if (!_spelGestart)
            {
                _spelGestart = true;
                _resterendeTijdTimer.Start();
            }
            else
            {
                UpdateTimer();
            }

            btnSteen.BorderBrush = new SolidColorBrush(Colors.SeaGreen);
            btnSteen.BorderThickness = new Thickness(2, 0, 0, 0);

            btnSchaar.BorderThickness = new Thickness(0);
            btnBlad.BorderThickness = new Thickness(0);

            CheckRondeWinnaar();
        }

        private void BtnBlad_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.BLAD;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            
            if (!_spelGestart)
            {
                _spelGestart = true;
                _resterendeTijdTimer.Start();
            }
            else
            {
                UpdateTimer();
            }

            btnBlad.BorderBrush = new SolidColorBrush(Colors.SeaGreen);
            btnBlad.BorderThickness = new Thickness(2, 0, 0, 0);

            btnSchaar.BorderThickness = new Thickness(0);
            btnSteen.BorderThickness = new Thickness(0);

            CheckRondeWinnaar();

        }

        private void BtnSchaar_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.SCHAAR;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            if (!_spelGestart)
            {
                _spelGestart = true;
                _resterendeTijdTimer.Start();
            }
            else
            {
                UpdateTimer();
            }


            btnSchaar.BorderBrush = new SolidColorBrush(Colors.SeaGreen);
            btnSchaar.BorderThickness = new Thickness(2, 0, 0, 0);

            btnSteen.BorderThickness = new Thickness(0);
            btnBlad.BorderThickness = new Thickness(0);

            CheckRondeWinnaar();

        }
        #endregion

        #region BUTTON EFFECTEN

        private void btnBlad_MouseEnter(object sender, MouseEventArgs e)
        {
            imgButtonBlad.Source = new BitmapImage(new Uri(@"img/blad_hover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnSteen_MouseEnter(object sender, MouseEventArgs e)
        {
            imgButtonSteen.Source = new BitmapImage(new Uri(@"img/steen_hover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnSchaar_MouseEnter(object sender, MouseEventArgs e)
        {
            imgButtonSchaar.Source = new BitmapImage(new Uri(@"img/schaar_hover.png", UriKind.RelativeOrAbsolute));
        }

        private void btnBlad_MouseLeave(object sender, MouseEventArgs e)
        {
            imgButtonBlad.Source = new BitmapImage(new Uri(@"img/blad.png", UriKind.RelativeOrAbsolute));
        }

        private void btnSteen_MouseLeave(object sender, MouseEventArgs e)
        {
            imgButtonSteen.Source = new BitmapImage(new Uri(@"img/steen.png", UriKind.RelativeOrAbsolute));
        }

        private void btnSchaar_MouseLeave(object sender, MouseEventArgs e)
        {
            imgButtonSchaar.Source = new BitmapImage(new Uri(@"img/schaar.png", UriKind.RelativeOrAbsolute));
        }

        #endregion

        #region GAME VERLOOP
        private void GenereerKeuzeComputer()
        {
            Random r = new Random();
            int willekeurig = r.Next(1, 4);
            _keuzeComputer = (Keuze)willekeurig;
        }

        private void CheckRondeWinnaar()
        {

            Brush win = new SolidColorBrush(Colors.Green);
            Brush verlies = new SolidColorBrush(Colors.Red);
            Brush gelijk = new SolidColorBrush(Colors.Gray);

            int speler = (int)_keuzeSpeler;
            int computer = (int)_keuzeComputer % 3;

            if (speler % 3 == computer)
            {
                UpdateBorder(gelijk, gelijk);
                ToonScore();
            }
            else if (speler == computer + 1)
            {
                UpdateBorder(win, verlies);
                _scoreSpeler++;
                ToonScore();
            }
            else
            {
                UpdateBorder(verlies, win);
                _scoreComputer++;
                ToonScore();
            }

            CheckSpelWinnaar();

        }

        private void CheckSpelWinnaar()
        {
            if(_scoreSpeler == 10)
            {
                _spelGestart = false;
                _resterendeTijdTimer.Stop();
                LblMelding.Content = "Proficiat, jij wint.";
            }
            if(_scoreComputer == 10)
            {
                _spelGestart = false;
                _resterendeTijdTimer.Stop();
                LblMelding.Content = "Helaas, de computer wint";
            }
            if(_scoreSpeler == 10 || _scoreComputer == 10)
            {
                MessageBoxResult nieuwSpel = MessageBox.Show("Wilt u een nieuw spel spelen?", "Nieuw spel?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if(nieuwSpel == MessageBoxResult.Yes)
                {
                    NieuwSpel();
                }
                else
                {
                    this.Close();
                }
            }
        }

        // Geeft de border de correcte kleur
        // GROEN = winst, ROOD = verlies, GRIJS = gelijkspel
        private void UpdateBorder(Brush user, Brush computer)
        {
            BdrSpeler.BorderThickness = new Thickness(0,0,0,2);
            BdrSpeler.BorderBrush = user;

            BdrComputer.BorderThickness = new Thickness(0,0,0,2);
            BdrComputer.BorderBrush = computer;
        }

        // Reset het volledige spel naar de beginwaarden om zo een nieuw spel te starten
        private void NieuwSpel()
        {
            _scoreSpeler = 0;
            _scoreComputer = 0;
            _resterendeSeconden = 3;
            LblSpelerNaam.Content = _spelerNaam;
            LblSpelerScore.Content = _scoreSpeler;
            LblComputerScore.Content = _scoreComputer;
            LblResterendeSeconden.Content = _resterendeSeconden;
            LblMelding.Content = "";
            BdrSpeler.BorderThickness = new Thickness(0);
            BdrComputer.BorderThickness = new Thickness(0);
            ImgKeuzeComputer.Source = null;
            ImgKeuzeSpeler.Source = null;
            btnSteen.BorderThickness = new Thickness(0);
            btnSchaar.BorderThickness = new Thickness(0);
            btnBlad.BorderThickness = new Thickness(0);
        }

        private void ToonScore()
        {
            LblSpelerScore.Content = _scoreSpeler;
            LblComputerScore.Content = _scoreComputer;
        }
        #endregion

        #region TOON KEUZE AFBEELDINGEN
        private void ToonAfbeeldingen()
        {
            string speler = _keuzeSpeler.ToString().ToLower();
            string computer = _keuzeComputer.ToString().ToLower();

            ImgKeuzeSpeler.Source = new BitmapImage(new Uri($"img/{speler}.png", UriKind.RelativeOrAbsolute));
            ImgKeuzeComputer.Source = new BitmapImage(new Uri($"img/{computer}.png", UriKind.RelativeOrAbsolute));
        }
        #endregion

        #region MENU ACTIES

        private void MnuSluiten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MnuRegels_Click(object sender, RoutedEventArgs e)
        {
            Spelregels regels = new Spelregels();
            regels.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Wil je echt afsluiten?", "Spel afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
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
    }
}
