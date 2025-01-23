using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjektZaliczeniowy;
using static ProjektZaliczeniowy.Magazyn;

namespace Gui
{
    /// <summary>
    /// Interaction logic for MagazynWindow.xaml
    /// </summary>
    public partial class MagazynWindow : Window
    {
        private Magazyn? magazyn;
        public MagazynWindow()
        {
            InitializeComponent();
            magazyn = Magazyn.OdczytajDCXML("magazyn.xml");

            if (magazyn != null)
            {
                lstKsiazki.ItemsSource = new ObservableCollection<Ksiazka>(magazyn.Ksiazki);

                long maxIsbn = magazyn.Ksiazki.Max(k => k.Isbn);

                var PoleGlobalIsbn = typeof(Ksiazka).GetField("globalIsbn",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);

                if (PoleGlobalIsbn != null)
                {
                    PoleGlobalIsbn.SetValue(null, maxIsbn + 1);
                }
            }

            //uzywam enum do wypełnienia listy
            cmbRodzajFantastyki.ItemsSource = Enum.GetValues(typeof(Fantastyka.EnumRodzaj));
            cmbRodzajKryminalu.ItemsSource = Enum.GetValues(typeof(Kryminal.EnumRodzaj));
            cmbRodzajLiteraturyFaktu.ItemsSource = Enum.GetValues(typeof(LiteraturaFaktu.EnumRodzaj));
            cmbRodzajRomansu.ItemsSource = Enum.GetValues(typeof(Romans.EnumRodzaj));
            cmbRodzajPodrecznika.ItemsSource = Enum.GetValues(typeof(ProjektZaliczeniowy.EnumPrzedmiot));
            cmbKlasaPodrecznika.ItemsSource = Enum.GetValues(typeof(ProjektZaliczeniowy.EnumKlasa));
        }


        private async void DodajKsiazke_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ProcesDodawaniaKsiazki.Visibility = Visibility.Visible;
                for (int i = 0; i <= 100; i += 20)
                {
                    ProcesDodawaniaKsiazki.Value = i;
                    await Task.Delay(200);
                }

                if (DodajRodzajKsiazki.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wybrać typ książki.");
                    return;
                }

                string wybranyTyp = ((ComboBoxItem)DodajRodzajKsiazki.SelectedItem).Content.ToString();


                string tytul = txtTytul.Text.Trim(); //Trim usunie biale znaki - niepotrzebne spacje itp.
                string autor = txtAutor.Text.Trim();

                if (string.IsNullOrWhiteSpace(tytul) || string.IsNullOrWhiteSpace(autor))
                {
                    MessageBox.Show("Proszę wprowadzić tytuł i autora.");
                    return;
                }

                if (!int.TryParse(txtIlosc.Text.Trim(), out int ilosc))
                {
                    MessageBox.Show("Proszę wprowadzić poprawną liczbę jako ilość.");
                    return;
                }

                if (!decimal.TryParse(txtCenaBazowa.Text.Trim(), out decimal cenaBazowa))
                {
                    MessageBox.Show("Proszę wprowadzić poprawną kwotę jako cenę bazową.");
                    return;
                }

                if (!int.TryParse(txtLiczbaStron.Text.Trim(), out int liczbaStron))
                {
                    MessageBox.Show("Proszę wprowadzić poprawną liczbę jako liczbę stron.");
                    return;
                }

                string dataWydania = txtDataWydania.Text.Trim();
                if (string.IsNullOrWhiteSpace(dataWydania))
                {
                    MessageBox.Show("Proszę wprowadzić datę wydania.");
                    return;
                }

                Ksiazka nowaKsiazka = wybranyTyp switch
                {
                    "Romans" => new Romans(tytul, autor, cenaBazowa, dataWydania, liczbaStron, PobierzRodzaj(cmbRodzajRomansu)),
                    "Kryminał" => new Kryminal(tytul, autor, cenaBazowa, dataWydania, liczbaStron, PobierzRodzaj(cmbRodzajKryminalu)),
                    "Fantastyka" => new Fantastyka(tytul, autor, cenaBazowa, dataWydania, liczbaStron, PobierzRodzaj(cmbRodzajFantastyki)),
                    "Literatura Faktu" => new LiteraturaFaktu(tytul, autor, cenaBazowa, dataWydania, liczbaStron, PobierzRodzaj(cmbRodzajLiteraturyFaktu)),
                    "Podręcznik" => new Podrecznik(tytul, autor, cenaBazowa, dataWydania, liczbaStron, PobierzRodzaj(cmbRodzajPodrecznika), PobierzRodzaj(cmbKlasaPodrecznika)),
                    _ => throw new ArgumentException("Nieznany typ książki.")
                };

                magazyn.DodajDoMagazynu(nowaKsiazka);

                // Odświeżam liste 
                lstKsiazki.ItemsSource = null;
                lstKsiazki.ItemsSource = magazyn.Ksiazki;

                MessageBox.Show($"Książka '{nowaKsiazka.Tytul}' została dodana. ISBN: {nowaKsiazka.Isbn}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
            finally
            {
                ProcesDodawaniaKsiazki.Visibility = Visibility.Hidden;
            }
        }

        private string PobierzRodzaj(ComboBox comboBox)
        {
            if (comboBox.SelectedItem == null)
            {
                throw new ArgumentException("Proszę wybrać rodzaj dla wybranego typu książki.");
            }
            return comboBox.SelectedItem.ToString();
        }


        private void WartoscMagazynu_Click(object sender, RoutedEventArgs e)
        {

            decimal wartoscMagazynu = magazyn.ObliczWartosc();
            MessageBox.Show($"Wartość magazynu: {wartoscMagazynu:C}", "Wartość magazynu", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Drukuj_Click(object sender, RoutedEventArgs e)
        {

            PrintDialog printDialog = new PrintDialog();

            // Sprawdzenie, czy użytkownik wybrał drukarkę
            if (printDialog.ShowDialog() == true)
            {
                // Zawartość do wydrukowania - przekształcam na tekst
                string contentToPrint = string.Join(Environment.NewLine, lstKsiazki.Items.Cast<Ksiazka>().Select(k => k.ToString()));

                FlowDocument flowDocument = new FlowDocument(new Paragraph(new Run(contentToPrint)))
                {
                    FontSize = 14,
                    PageWidth = printDialog.PrintableAreaWidth,
                    PageHeight = printDialog.PrintableAreaHeight
                };


                IDocumentPaginatorSource paginator = flowDocument;
                printDialog.PrintDocument(paginator.DocumentPaginator, "Wydruk");

                MessageBox.Show("Drukowanie...", "Wydruk", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Wstecz_Click(object sender, RoutedEventArgs e)
        {
            Heaven heaven = new Heaven();
            heaven.Show();
            this.Close();
        }



        private void lstKsiazki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && (textBox.Text == "Wpisz tytuł..." ||
                                    textBox.Text == "Wpisz autora..." ||
                                    textBox.Text == "Wpisz ilość..." ||
                                    textBox.Text == "Wpisz datę wydania..." ||
                                    textBox.Text == "Wpisz cenę bazową..." ||
                                    textBox.Text == "Wpisz liczbę stron..."))
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (textBox.Name == "txtTytul") textBox.Text = "Wpisz tytuł...";
                else if (textBox.Name == "txtAutor") textBox.Text = "Wpisz autora...";
                else if (textBox.Name == "txtIlosc") textBox.Text = "Wpisz ilość...";
                else if (textBox.Name == "txtDataWydania") textBox.Text = "Wpisz datę wydania...";
                else if (textBox.Name == "txtCenaBazowa") textBox.Text = "Wpisz cenę bazową...";
                else if (textBox.Name == "txtLiczbaStron") textBox.Text = "Wpisz liczbę stron...";

                textBox.Foreground = Brushes.Gray;
            }
        }


        private void RodzajKsiazki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRodzaj = (RodzajKsiazki.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Tworzymy pustą listę książek
            List<Ksiazka> filteredBooks = new List<Ksiazka>();

            // Filtrowanie książek na podstawie ich typu
            if (selectedRodzaj == "Fantastyka")
            {
                filteredBooks = magazyn?.ksiazki.OfType<Fantastyka>().Cast<Ksiazka>().ToList();
            }
            else if (selectedRodzaj == "Podręcznik")
            {
                filteredBooks = magazyn?.ksiazki.OfType<Podrecznik>().Cast<Ksiazka>().ToList();
            }
            else if (selectedRodzaj == "Literatura Faktu")
            {
                filteredBooks = magazyn?.ksiazki.OfType<LiteraturaFaktu>().Cast<Ksiazka>().ToList();
            }
            else if (selectedRodzaj == "Kryminał")
            {
                filteredBooks = magazyn?.ksiazki.OfType<Kryminal>().Cast<Ksiazka>().ToList();
            }
            else if (selectedRodzaj == "Romans")
            {
                filteredBooks = magazyn?.ksiazki.OfType<Romans>().Cast<Ksiazka>().ToList();
            }
            else if (selectedRodzaj == "Wszystko")
            {
                filteredBooks = magazyn?.ksiazki ?? new List<Ksiazka>();
            }

            // Wyświetlanie przefiltrowanych książek w ListBoxie
            lstKsiazki.ItemsSource = new ObservableCollection<Ksiazka>(filteredBooks);
        }


        private void Usun_Click(object sender, RoutedEventArgs e)
        {

            Ksiazka wybranaKsiazka = (Ksiazka)lstKsiazki.SelectedItem;
            if (wybranaKsiazka == null)
            {
                MessageBox.Show("Proszę wybrać książkę do usunięcia.");
                return;
            }

            try
            {

                magazyn.UsunKsiazke(wybranaKsiazka.Isbn);



                lstKsiazki.ItemsSource = null; 
                lstKsiazki.ItemsSource = magazyn.Ksiazki;
                magazyn.ZapiszDCXML("magazyn.xml");


                MessageBox.Show($"Książka '{wybranaKsiazka.Tytul}' została usunięta z magazynu.");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Sortuj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string wybraneSortowanie = (Sortuj.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (wybraneSortowanie == "Cena rosnąco")
            {
                magazyn.SortujKsiazkiPoCenieRosnaco();
            }
            else if (wybraneSortowanie == "Cena malejąco")
            {
                magazyn.SortujKsiazkiPoCenieMalejaco();
            }
            else if (wybraneSortowanie == "Alfabetycznie")
            {
                magazyn.SortujKsiazkiPoTytule();
            }


            lstKsiazki.ItemsSource = null; 
            lstKsiazki.ItemsSource = magazyn.ksiazki;
        }



        private void DodajRodzajKsiazki_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbRodzajPodrecznika == null || cmbKlasaPodrecznika == null ||
                cmbRodzajRomansu == null || cmbRodzajKryminalu == null ||
                cmbRodzajFantastyki == null || cmbRodzajLiteraturyFaktu == null)
            {
                return;
            }


            cmbRodzajRomansu.Visibility = Visibility.Collapsed;
            cmbRodzajKryminalu.Visibility = Visibility.Collapsed;
            cmbRodzajFantastyki.Visibility = Visibility.Collapsed;
            cmbRodzajLiteraturyFaktu.Visibility = Visibility.Collapsed;
            cmbRodzajPodrecznika.Visibility = Visibility.Collapsed;
            cmbKlasaPodrecznika.Visibility = Visibility.Collapsed;


            if (DodajRodzajKsiazki.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Podręcznik":
                        cmbRodzajPodrecznika.Visibility = Visibility.Visible;
                        cmbKlasaPodrecznika.Visibility = Visibility.Visible;
                        break;
                    case "Romans":
                        cmbRodzajRomansu.Visibility = Visibility.Visible;
                        break;
                    case "Kryminał":
                        cmbRodzajKryminalu.Visibility = Visibility.Visible;
                        break;
                    case "Fantastyka":
                        cmbRodzajFantastyki.Visibility = Visibility.Visible;
                        break;
                    case "Literatura Faktu":
                        cmbRodzajLiteraturyFaktu.Visibility = Visibility.Visible;
                        break;
                }
            }
        }



    }
}