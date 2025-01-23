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
using ProjektZaliczeniowy;

namespace Gui
{
    /// <summary>
    /// Interaction logic for Wyszukaj.xaml
    /// </summary>
    public partial class Wyszukaj : Window

    {
        private Magazyn? magazyn;


        public Wyszukaj()
        {
            InitializeComponent();

            try
            {
                magazyn = Magazyn.OdczytajDCXML("magazyn.xml");
                if (magazyn is not null)
                {
                    listaKsiazek.ItemsSource = new ObservableCollection<Ksiazka>(magazyn.Ksiazki);
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

        private void Rodzaj_Click(object sender, RoutedEventArgs e)
        {


        }

        private void listaKsiazek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Rodzaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Rodzaj.SelectedItem is ComboBoxItem selectedItem)
            {
  
                string wybranyRodzaj = (selectedItem.Content as string);

                if (!string.IsNullOrEmpty(wybranyRodzaj))
                {
 
                    IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoRodzaju(wybranyRodzaj);

                    listaKsiazek.ItemsSource = wyniki;
                }
            }
        }

        private void Przedmiot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Klasa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Klasa.SelectedItem is ComboBoxItem selectedItem)
            {

                string wybranaKlasa = selectedItem.Content as string;

                if (!string.IsNullOrEmpty(wybranaKlasa))
                {

                    if (Enum.TryParse(wybranaKlasa, out EnumKlasa klasaEnum))
                    {

                        IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoKlasie(klasaEnum);


                        listaKsiazek.ItemsSource = wyniki;
                    }
                }
            }
        }

        private void Przedmiot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Przedmiot.SelectedItem is ComboBoxItem selectedItem)
            {
 
                string wybranyPrzedmiot = selectedItem.Content as string;

                if (!string.IsNullOrEmpty(wybranyPrzedmiot))
                {

                    if (Enum.TryParse(wybranyPrzedmiot, out EnumPrzedmiot przedmiotEnum))
                    {

                        IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoPrzedmiocie(przedmiotEnum);

                   
                        listaKsiazek.ItemsSource = wyniki;
                    }
                }
            }
        }

        private void Szukaj_Click(object sender, RoutedEventArgs e)
        {
    
            string autor = Autor.Text;

            if (!string.IsNullOrEmpty(autor))
            {
               
                IEnumerable<Ksiazka> wyniki = magazyn.WyszukajPoAutorze(autor);

               
                listaKsiazek.ItemsSource = wyniki;
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić nazwisko autora.");
            }
        }

        private void Autor_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}