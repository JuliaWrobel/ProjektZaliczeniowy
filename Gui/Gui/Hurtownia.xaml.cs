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
using Microsoft.Web.WebView2.Core;

namespace Gui
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Hurtownia : Window
    {
        public Hurtownia()
        {
            InitializeComponent();

            webView.EnsureCoreWebView2Async(null).ContinueWith(task =>
            {
                if (task.Exception != null)
                {

                    MessageBox.Show("Wystąpił problem z inicjalizowaniem WebView2.");
                }
                else
                {

                    webView.CoreWebView2.Navigate("https://hurtksiazki.pl/");
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void Wstecz_Click(object sender, RoutedEventArgs e)
        {
            Heaven heaven = new Heaven();
            heaven.Show();
            this.Close();
        }
    }
}
