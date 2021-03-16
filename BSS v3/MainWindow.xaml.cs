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
        private DispatcherTimer tijdTonen = new DispatcherTimer();
        private DispatcherTimer spelTimer;
        private Keuze _keuzeSpeler;
        private Keuze _keuzeComputer;
        private Rectangle _rechthoekSpeler;
        private Rectangle _rechthoekComputer;
        private int _scoreSpeler;
        private int _scoreComputer;
        private int _spelSeconden;
        private string _spelerNaam;
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

            tijdTonen.Interval = TimeSpan.FromMilliseconds(1000);
            tijdTonen.Tick += timer_tick;
            tijdTonen.Start();

            StartSpelTimer();
        }
        #endregion

        private void timer_tick(object sender, EventArgs e)
        {
            LblToonTijd.Content = DateTime.Now.ToString("d MMMM yyyy HH:mm:ss");
        }

        #region SPEL TIJDLIMIET
        private void StartSpelTimer()
        {
            spelTimer = new DispatcherTimer();
            _spelSeconden = 10;
            LblResterendeSeconden.Content = _spelSeconden;
            spelTimer.Interval = TimeSpan.FromMilliseconds(1000);
            spelTimer.Tick += spelTimer_tick;
        }

        private void UpdateTimer()
        {
            _spelSeconden = 10;
            LblResterendeSeconden.Content = _spelSeconden;
        }

        private void spelTimer_tick(object sender, EventArgs e)
        {
            _spelSeconden--;
            if (_spelSeconden == 0)
            {
                MessageBox.Show("U was te laat, u hebt verloren.", "Te laat", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                spelTimer.Stop();
                _spelGestart = false;
                NieuwSpel();
            }
            LblResterendeSeconden.Content = _spelSeconden;
        }
        #endregion

        #region BUTTON KEUZES
        private void BtnSteen_Click(object sender, RoutedEventArgs e)
        {
            _keuzeSpeler = Keuze.STEEN;
            GenereerKeuzeComputer();
            ToonAfbeeldingen();
            CheckWinnaar();
            if (!_spelGestart)
            {
                _spelGestart = true;
                spelTimer.Start();
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
                spelTimer.Start();
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
                spelTimer.Start();
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

        private void UpdateRechthoek(Brush user, Brush computer)
        {
            _rechthoekSpeler.Stroke = user;
            _rechthoekComputer.Stroke = computer;
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

        private void NieuwSpel()
        {
            TekenRechthoek();
            _rechthoekSpeler.Stroke = new SolidColorBrush(Colors.Gray);
            _rechthoekComputer.Stroke = new SolidColorBrush(Colors.Gray);
            _scoreSpeler = 0;
            _scoreComputer = 0;
            _spelSeconden = 10;
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
    }
}
