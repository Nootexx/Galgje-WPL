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
using System.Threading;
using System.Windows.Threading;

namespace Galgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public string geheimeWoord;
        public string raadPoging;
        public string foutLetters;
        public string juistLetters;
        public int levens;
        public bool spelBegonnen;
        char letter;
        //bool letterIsInWoord = false;
        char[] arraySterren;
        //string woordTest = new string(arraySterren);
        int timerSeconden;
        bool gewonnen;
        public DispatcherTimer timerSpel = new DispatcherTimer();
        bool RefreshKeyboard;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitSpel();
            timerSpel.Tick += TimerSpel_Tick;
            timerSpel.Interval = TimeSpan.FromSeconds(1);
            ImgMan.Source= new BitmapImage(new Uri($@"/Images/10.jpg", UriKind.Relative));
        }

        private void TimerSpel_Tick(object sender, EventArgs e)
        {
            timerSeconden--;
            TxtBTimer.Text = $"{timerSeconden}";
            switch (timerSeconden)
            {
                case 10:
                    TxtBTimer.Foreground = Brushes.Black;
                    TxtBTimer.FontSize = 16;
                    break;
                case 3:
                    TxtBTimer.Foreground = Brushes.Red;
                    TxtBTimer.FontSize = 18;
                    break;
                case 2:
                    TxtBTimer.Foreground = Brushes.Red;
                    TxtBTimer.FontSize = 20;
                    break;
                case 1:
                    TxtBTimer.Foreground = Brushes.Red;
                    TxtBTimer.FontSize = 21;
                    break;
                case 0:
                    TxtBTimer.Foreground = Brushes.Red;
                    TxtBTimer.FontSize = 22;
                    VerminderingVanLeven();
                    Thread.Sleep(100);
                    timerSeconden = 11;
                    break;
                default:
                    TxtBTimer.Foreground = Brushes.Black;
                    TxtBTimer.FontSize = 16;
                    break;
            }
        }

        private void TimerRestart()
        {
            timerSpel.Stop();
            TxtBTimer.Text = string.Empty;
            timerSeconden = 11;
            timerSpel.Start();
        }

        public void InitSpel()
        {
            // Deze methode bevat alle nodige fucties om het spel te innitaliseren en ervoor zorgen dat alles juist is ingesteld
            // om het volgende potje foutloos aftelopen.

            TxtBInfo.Text = "Geef een geheim woord in:";
            TxtInput.Clear();
            TxtRaad.Clear();
            StcRaad.Visibility = Visibility.Hidden;
            TxtInput.IsEnabled = true;
            BtnRaad.Visibility = Visibility.Hidden;
            BtnNieuwSpel.Visibility = Visibility.Hidden;
            ImgMan.Visibility = Visibility.Hidden;
            BtnVerbergWoord.Visibility = Visibility.Visible;
            TxtInput.Focus();
            levens = 10;
            ManWordGehangen();
            geheimeWoord = string.Empty;
            juistLetters = string.Empty;
            foutLetters = string.Empty;
            RefreshKeyboard = false;
            timerSpel.Stop();
            timerSeconden = 11;
            TxtBTimer.Text = string.Empty;
            TxtBTimer.Visibility = Visibility.Hidden;
            TxtBTimer.Foreground = Brushes.Black;
            TxtBTimer.FontSize = 16;
        }
        string sterretjes = "";

        private void ltrStreep()
        {
            for (int i = 0; i < geheimeWoord.Length; i++)
            {
                sterretjes += "*";
            }
            tbTestDiego.Text = sterretjes;
        }

        public void RaadStatus()
        {
            // Deze methode is actief wanner het speler een woord is aan het raden en beindigd wanneer het woord geraden is

            // In deze methode word elke letter van het geheime woord omgezet naar een *
            // Een array word hiervoor gebruikt en het vullen gebeurt met een foreach loop

            TxtInput.IsEnabled = false;
            StcRaad.Visibility = Visibility.Visible;
            geheimeWoord = TxtInput.Text.ToLower();

            char[] lettersGeheimWoord = geheimeWoord.ToCharArray();
            arraySterren = new char[geheimeWoord.Length];
            int x = 0;
            foreach (char c in lettersGeheimWoord)
            {
                arraySterren[x] = '*';
                x++;
            }
            TxtInput.Text = new string(arraySterren);

            BtnVerbergWoord.Visibility = Visibility.Hidden;
            ImgMan.Visibility = Visibility.Visible;
            TxtBInfo.Text = $"{levens} Levens";
        }

        //public void LetterGeraden()
        //{
        //    char[] lettersGeheimWoord = geheimeWoord.ToCharArray();
        //    //bool letterIsInWoord = false;
        //    char charRaadPoging;
        //    bool convToChar = char.TryParse(raadPoging, out charRaadPoging);

        //    if (convToChar)
        //    {
        //        foreach (char c in lettersGeheimWoord)
        //        {
        //            if (c.Equals(charRaadPoging))
        //            {
        //                letterIsInWoord = true;
        //            }
        //        }
        //    }
        //    else if (!convToChar)
        //    {
        //        MessageBox.Show("Geef een letter of woord in");
        //    }


        //    if (letterIsInWoord && convToChar)
        //    {
        //        if (juistLetters == "")
        //        {
        //            juistLetters += $"{raadPoging}";
        //        }
        //        else
        //        {
        //            juistLetters += $",{raadPoging}";
        //        }
        //        TxtInput.Clear();
        //        TxtBInfo.Text = $"{levens} Levens\nJuiste letters {juistLetters}\nFoute letters {foutLetters}";
        //    }
        //    else if (!letterIsInWoord && convToChar)
        //    {
        //        --levens;
        //        if (foutLetters == "")
        //        {
        //            foutLetters += $"{raadPoging}";
        //        }
        //        else
        //        {
        //            foutLetters += $",{raadPoging}";
        //        }
        //        TxtInput.Clear();
        //        TxtBInfo.Text = $"{levens} Levens\nJuiste letters {juistLetters}\nFoute letters {foutLetters}";
        //    }
        //}

        //public void RaadPoging()
        //{
        //    raadPoging = TxtInput.Text;

        //    if (raadPoging.Length > 1)
        //    {
        //        if (raadPoging != geheimeWoord)
        //        {
        //            --levens;
        //            if (foutLetters == "")
        //            {
        //                foutLetters += $"{raadPoging}";
        //            }
        //            else
        //            {
        //                foutLetters += $",{raadPoging}";
        //            }
        //            TxtInput.Clear();
        //            TxtBInfo.Text = $"{levens} Levens\nJuiste letters {juistLetters}\nFoute letters {foutLetters}";
        //        }
        //        else if (raadPoging == geheimeWoord)
        //        {
        //            MessageBox.Show("Gefeliciteerd!\nje hebt juist geraden");
        //            InitSpel();
        //        }
        //    }
        //    else
        //    {
        //        LetterGeraden();
        //    }
        //}

        private BitmapImage ImgSource(int imgNumber)
        {
            // Deze methode zorgt ervoor dat een source link word aangemaakt voor het image paneel binnenin het XAML.
            return new BitmapImage(new Uri($@"/Images/{imgNumber}.jpg", UriKind.Relative));
        }

        private void ManWordGehangen()
        {
            // Deze methode wordt gebruikt om het verschillende images te tonen van het mannetje.
            // Er word gekeken naar het aantal levens en hiermee wordt bepaald welke stage van het ophanging word getoond.

            switch (levens)
            {
                case 10:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 9:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 8:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 7:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 6:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 5:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 4:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 3:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 2:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 1:
                    ImgMan.Source = ImgSource(levens);
                    break;
                case 0:
                    ImgMan.Source = ImgSource(levens);
                    break;
                default:
                    break;
            }
        }

        private void VerminderingVanLeven()
        {
            while (levens > 0)
            {
                levens--;
                TxtBInfo.Text = $"{levens} Levens";
                ManWordGehangen();
                break;
            }
            if (levens < 1)
            {
                gewonnen = false;
                EindSpelScherm(gewonnen);
            }
        }


        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            InitSpel();
        }
        string juisteLetters = "";
        private void BtnRaad_Click(object sender, RoutedEventArgs e)
        {
            //RaadPoging();

            ManWordGehangen();
            while (levens > 0 && !string.IsNullOrWhiteSpace(TxtRaad.Text))
            {
                if (TxtRaad.Text.Length >= 2)
                {
                    if (TxtRaad.Text == geheimeWoord)
                    {
                        gewonnen = true;
                        EindSpelScherm(gewonnen);
                    }
                    else
                    {
                        VerminderingVanLeven();
                        ManWordGehangen();
                        TxtBInfo.Text = $"{levens} Levens";
                    }
                }
                else
                {
                    if (geheimeWoord.Contains(TxtRaad.Text))
                    {
                        juisteLetters += TxtRaad.Text;
                        Test(TxtRaad.Text);
                    }
                    else
                    {
                        //-1 leven
                    }
                    if(sterretjes == geheimeWoord)
                    {
                        gewonnen = true;
                        EindSpelScherm(gewonnen);
                    }
                }
               
                break;
            }
            TxtRaad.Clear();
        }

        private void Test(string letter)
        {
            int index = geheimeWoord.IndexOf(letter);
            while (index >= 0)
            {
                sterretjes = sterretjes.Remove(index, 1);
                sterretjes = sterretjes.Insert(index, letter);
                index = geheimeWoord.IndexOf(letter, index + 1);
            }
            TxtInput.Text = sterretjes;
        }

        private void BtnVerbergWoord_Click(object sender, RoutedEventArgs e)
        {
            if (TxtInput.Text.Length > 1 && !string.IsNullOrWhiteSpace(TxtInput.Text))
            {
                RaadStatus();
                ltrStreep();
                RefreshKeyboard = true;
                TxtBTimer.Visibility = Visibility.Visible;
                timerSpel.Start();
                BtnRaad.Visibility = Visibility.Visible;
                BtnNieuwSpel.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Vul een woord in om het spel te beginnen");
            }
            TxtInput.Focus();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            letter = char.Parse(((Button)sender).Content.ToString().ToLower());
            //char[] lettersGeheimWoord = geheimeWoord.ToCharArray();

            while (levens > 0)
            {
                if (geheimeWoord.ToLower().Contains(letter))
                {
                    ((Button)sender).Background = Brushes.Green;
                    ((Button)sender).BorderBrush = Brushes.DarkGreen;
                    ((Button)sender).IsHitTestVisible = false;
                    TxtInput.Text = ToonGeheimeWoord(letter);
                    TxtBInfo.Text = $"{levens} Levens";

                    // In deze if statement word er gekeken als het arraySteren(geheimewoord) * bevat zoniet heeft het speler alle
                    // letters geraden en heeft hij in vervolg ook het woord geraden dus het spel beindingt met een proficiat scherm.

                    if (!arraySterren.Contains('*'))
                    {
                        gewonnen = true;
                        EindSpelScherm(gewonnen);
                    }
                    TimerRestart();

                }
                else
                {
                    ((Button)sender).Background = Brushes.Red;
                    ((Button)sender).BorderBrush = Brushes.DarkRed;
                    ((Button)sender).IsHitTestVisible = false;
                    VerminderingVanLeven();
                    TxtBInfo.Text = $"{levens} Levens";
                    ManWordGehangen();
                    TimerRestart();
                }
                break;
            }
        }

        private string ToonGeheimeWoord(char c)
        {
            // Deze methode zorgt ervoor dat het geraden letter word vervangen met een * binnenin het geheimewoord.

            char[] charArrayGeheimWoord = geheimeWoord.ToCharArray();

            for (int i = 0; i < geheimeWoord.Length; i++)
            {
                if (charArrayGeheimWoord[i] == c)
                {
                    arraySterren[i] = c;
                }
            }

            return new string(arraySterren);
        }



        private void EindSpelScherm(bool gewonnenCheck)
        {
            timerSpel.Stop();
            if (gewonnenCheck)
            {
                MessageBox.Show($"proficiat het woord was: { geheimeWoord}");
            }
            else
            {
                MessageBox.Show($"Je hebt verloren\nHet juiste woord was: {geheimeWoord}");
            }
            TxtBTimer.Visibility = Visibility.Hidden;
            InitSpel();
        }

    }
}
