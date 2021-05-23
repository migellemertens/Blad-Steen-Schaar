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
using System.Threading;

namespace BSS_v2
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
        private Rectangle _rechthoekSpeler;
        private Rectangle _rechthoekComputer;
        private int _scoreSpeler;
        private int _scoreComputer;
        private int _resterendeSeconden;
        private string _spelerNaam;
        private int _pauze = 3;
        private bool _spelGestart = false;
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
            if(_pauze == 0)
            {
                _pauzeNaTeLaatTimer.Stop();
                _pauze = 3;
                LblTeLaatMelding.Visibility = Visibility.Hidden;
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

                LblTeLaatMelding.Visibility = Visibility.Visible;
                _scoreComputer++;
                LblComputerScore.Content = _scoreComputer;

                UpdateRechthoek(new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.Green));

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
            CheckWinnaar();
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

        }

        private void BtnBlad_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.BLAD;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            CheckWinnaar();
            if (!_spelGestart)
            {
                _spelGestart = true;
                _resterendeTijdTimer.Start();
            }
            else
            {
                UpdateTimer();
            }

        }

        private void BtnSchaar_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.SCHAAR;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            CheckWinnaar();
            if (!_spelGestart)
            {
                _spelGestart = true;
                _resterendeTijdTimer.Start();
            }
            else
            {
                UpdateTimer();
            }

        }
        #endregion

        #region GAME VERLOOP
        private void GenereerKeuzeComputer()
        {
            Random r = new Random();
            int willekeurig = r.Next(1, 4);
            _keuzeComputer = (Keuze)willekeurig;
        }

        private void CheckWinnaar()
        {
            TekenRechthoek();

            Brush win = new SolidColorBrush(Colors.Green);
            Brush verlies = new SolidColorBrush(Colors.Red);
            Brush gelijk = new SolidColorBrush(Colors.Gray);

            int speler = (int)_keuzeSpeler;
            int computer = (int)_keuzeComputer % 3;

            if (speler % 3 == computer)
            {
                UpdateRechthoek(gelijk, gelijk);
                ToonScore();
            }
            else if (speler == computer + 1)
            {
                UpdateRechthoek(win, verlies);
                _scoreSpeler++;
                ToonScore();
            }
            else
            {
                UpdateRechthoek(verlies, win);
                _scoreComputer++;
                ToonScore();
            }

        }

        // Tekents de rechthoeken rond de keuze van de speler
        private void TekenRechthoek()
        {
            _rechthoekSpeler = new Rectangle()
            {
                Width = 250,
                Height = 250,
                StrokeThickness = 7,
                Margin = new Thickness(0, 0, 0, 0),
                Stroke = new SolidColorBrush(Colors.Gray)
            };

            CanAfbeelding.Children.Add(_rechthoekSpeler);

            _rechthoekComputer = new Rectangle()
            {
                Width = 250,
                Height = 250,
                StrokeThickness = 7,
                Margin = new Thickness(270, 0, 0, 0),
                Stroke = new SolidColorBrush(Colors.Gray)
            };

            CanAfbeelding.Children.Add(_rechthoekComputer);
        }

        // Geeft de rechthoek de correcte kleur
        // GROEN = winst, ROOD = verlies, GRIJS = gelijkspel
        private void UpdateRechthoek(Brush user, Brush computer)
        {
            _rechthoekSpeler.Stroke = user;
            _rechthoekComputer.Stroke = computer;
        }

        // Reset het volledige spel naar de beginwaarden om zo een nieuw spel te starten
        private void NieuwSpel()
        {
            TekenRechthoek();
            _rechthoekSpeler.Stroke = new SolidColorBrush(Colors.Gray);
            _rechthoekComputer.Stroke = new SolidColorBrush(Colors.Gray);
            _scoreSpeler = 0;
            _scoreComputer = 0;
            _resterendeSeconden = 3;
            LblSpelerNaam.Content = _spelerNaam;
            LblSpelerScore.Content = _scoreSpeler;
            LblComputerScore.Content = _scoreComputer;
            LblResterendeSeconden.Content = "";
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
            CanAfbeelding.Children.Clear();
            string speler = _keuzeSpeler.ToString().ToLower();
            string computer = _keuzeComputer.ToString().ToLower();

            BitmapImage userImage = new BitmapImage();
            userImage.BeginInit();
            userImage.UriSource = new Uri($"img\\{speler}.png", UriKind.RelativeOrAbsolute);
            userImage.EndInit();
            Image userChoice = new Image();
            userChoice.Source = userImage;
            userChoice.Margin = new Thickness(15, 23, 0, 0);
            userChoice.Width = 200;
            userChoice.Height = 200;
            CanAfbeelding.Children.Add(userChoice);

            BitmapImage computerImage = new BitmapImage();
            computerImage.BeginInit();
            computerImage.UriSource = new Uri($"img\\{computer}.png", UriKind.RelativeOrAbsolute);
            computerImage.EndInit();
            Image computerChoice = new Image();
            computerChoice.Source = computerImage;
            computerChoice.Margin = new Thickness(295, 23, 0, 0);
            computerChoice.Width = 200;
            computerChoice.Height = 200;
            CanAfbeelding.Children.Add(computerChoice);

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
    }
}
