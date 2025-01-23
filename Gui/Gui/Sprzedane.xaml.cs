using ProjektZaliczeniowy;
using System;
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

namespace Gui
{
    /// <summary>
    /// Interaction logic for Sprzedane.xaml
    /// </summary>
    public partial class Sprzedane : Window
    {
      public  Magazyn? magazyn;
        public Sprzedane()
        {
            InitializeComponent();

            try
            {
                magazyn = Magazyn.OdczytajDCXML("magazyn.xml");
                if (magazyn is not null)
                {
                    lista.ItemsSource = new ObservableCollection<Ksiazka>(magazyn.Ksiazki);
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

        private void Sprzedaj_Click(object sender, RoutedEventArgs e)
        {


            Ksiazka wybranaKsiazka = (Ksiazka)lista.SelectedItem;
            if (wybranaKsiazka == null)
            {
                MessageBox.Show("Proszę wybrać książkę.");
                return;
            }


            if (!int.TryParse(Ilosc.Text, out int ilosc) || ilosc <= 0)
            {
                MessageBox.Show("Proszę wprowadzić poprawną liczbę egzemplarzy (większą od 0).");
                return;
            }

            try
            {
 
                magazyn.Sprzedaj(wybranaKsiazka.Isbn, ilosc);
                lista.Items.Refresh();
                magazyn.ZapiszDCXML("magazyn.xml");


                MessageBox.Show($"Sprzedano {ilosc} egzemplarzy książki {wybranaKsiazka.Tytul}.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Wstecz_Click(object sender, RoutedEventArgs e)
        {
            Heaven heaven = new Heaven();
            heaven.Show();
            this.Close();
        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        
        {

            string tytul = Tytul.Text;

            if (!string.IsNullOrEmpty(tytul))
            {

                IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoTytule(tytul);

                lista.ItemsSource = wyniki;
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić tytuł.");
            }
        }
        
    }
}
