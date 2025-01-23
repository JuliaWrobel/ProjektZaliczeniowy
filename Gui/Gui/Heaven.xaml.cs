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

namespace Gui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Heaven : Window
    {
        public Heaven()
        {
            InitializeComponent();
        }


        private void Sprzedane_Click(object sender, RoutedEventArgs e)
        {
            Sprzedane sprzedane = new Sprzedane();
            sprzedane.Show();
            this.Close();
        }

        private void Magazyn_Click(object sender, RoutedEventArgs e)
        {
            MagazynWindow magazynWindow = new MagazynWindow();
            magazynWindow.Show();
            this.Close();

        }

        private void Dostawa_Click(object sender, RoutedEventArgs e)
        {
            Dostawa dostawa = new Dostawa();
            dostawa.Show();
            this.Close();
        }

        private void Wyszukaj_Click(object sender, RoutedEventArgs e)
        {
            Wyszukaj wyszukaj = new Wyszukaj();
            wyszukaj.Show();
            this.Close();
        }



        private void Wyloguj_Click(object sender, RoutedEventArgs e)
        {
            MainWindow logowanie = new MainWindow();
            logowanie.Show();
            this.Close();
        }


        private void Strona_Click(object sender, RoutedEventArgs e)
        {
            Hurtownia hurtownia = new Hurtownia();
            hurtownia.Show();
            this.Close();
        }
    }
}

