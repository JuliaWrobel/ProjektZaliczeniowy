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


namespace Gui
{
    /// <summary>
    /// Interaction logic for Dostawa.xaml
    /// </summary>
    public partial class Dostawa : Window
    {
        public Magazyn? magazyn;
        public Dostawa()
        {
            InitializeComponent();
            try
            {
                magazyn = Magazyn.OdczytajDCXML("magazyn.xml");
                if (magazyn is not null)
                {
                    listBook.ItemsSource = new ObservableCollection<Ksiazka>(magazyn.Ksiazki);
                }
                else
                {
                    throw new Exception("Magazyn jest pusty lub nie został poprawnie załadowany.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Wstecz_Click(object sender, RoutedEventArgs e)
        {
            Heaven heaven = new Heaven();
            heaven.Show();
            this.Close();
        }

        private void Dostawa_Click(object sender, RoutedEventArgs e)
        {

            Ksiazka wybranaKsiazka = (Ksiazka)listBook.SelectedItem;
            if (wybranaKsiazka == null)
            {
                MessageBox.Show("Proszę wybrać książkę z listy.");
                return;
            }


            int ilosc;
            if (!int.TryParse(Ilosc.Text, out ilosc) || ilosc <= 0)
            {
                MessageBox.Show("Proszę wprowadzić poprawną liczbę książek (większą od 0).");
                return;
            }

            try
            {
 
                magazyn.Dostawa(wybranaKsiazka.Isbn, ilosc);
                listBook.Items.Refresh();
                magazyn.ZapiszDCXML("magazyn.xml");


                MessageBox.Show($"Dostarczono {ilosc} egzemplarzy książki '{wybranaKsiazka.Tytul}'.");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





private void listBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {

            string tytul = Tytul.Text;

            if (!string.IsNullOrEmpty(tytul))
            {

                IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoTytule(tytul);


                listBook.ItemsSource = wyniki;
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić tytuł.");
            }
        }
    }
}