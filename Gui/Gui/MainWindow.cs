using System;
using System.Windows;
using System.Windows.Media;
using ProjektZaliczeniowy;

namespace Gui
{
    public partial class MainWindow : Window
    {

        private string correctUsername = "heaven";
        private string correctPassword = "123";

        public MainWindow()
        {
            InitializeComponent();


        }

        private void PlayLoginSound(string fileName)
        {


            try
            {
     
                Uri fileUri = new Uri($"Sounds/{fileName}", UriKind.Relative);

                MediaPlayer player = new MediaPlayer();
                player.Open(fileUri);
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd odtwarzania dźwięku: " + ex.Message);
            }
        }


        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username.Text;
            string password = Password.Password;

            // Sprawdzanie poprawności logowania
            if (username == correctUsername && password == correctPassword)
            {
                PlayLoginSound("level-up-191997.mp3"); // Dźwięk przy poprawnym logowaniu

                // Dodanie opóźnienia przed przejściem do nowego okna
                await Task.Delay(2000); 

                Heaven heaven = new Heaven();
                heaven.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Proszę wprowadzić poprawnne hasło!");
                PlayLoginSound("buzzer-or-wrong-answer-20582.mp3"); // Dźwięk przy błędnym logowaniu
            }
        }

    }
}
