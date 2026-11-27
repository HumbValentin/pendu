using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

namespace pendu
{
    
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool partieTerminee = false;

        List<string> mots = new List<string>();

        Random random = new Random();

        string motAleatoire;
        int lenght;

        DispatcherTimer montimer;
      

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the timer
            montimer = new DispatcherTimer();

            // Get the path to the Hollow_Mots.txt file
            string chemin = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "List", "Hollow_Mots.txt");

            // Check if the Hollow_Mots.txt file exists
            if (!File.Exists(chemin))
            {
                MessageBox.Show("Le fichier Hollow_Mots.txt est introuvable !");
                Application.Current.Shutdown();
                return;
            }


            if (!File.Exists(@"Resource/Sound/wow.mp3"))
            {
                MessageBox.Show("Le fichier son win est introuvable !");
                Application.Current.Shutdown();
                return;
            }

            if (!File.Exists(@"Resource/Sound/spongebob-fail.mp3"))
            {
                MessageBox.Show("Le fichier son fail est introuvable !");
                Application.Current.Shutdown();
                return;
            }





            // Get the path to the Hollow_Mots.txt file
            mots = File.ReadAllLines(chemin)
               .Where(m => !string.IsNullOrWhiteSpace(m))
               .Select(m => m.Trim().ToUpper())
               .ToList();


            // Check if the list of words is empty
            if (mots.Count == 0)
            {
                MessageBox.Show("Le fichier ne contient aucun mot !");
                Application.Current.Shutdown();
                return;
            }
            else
            {
                MessageBox.Show("Le Fichier contient " + mots.Count + "mots");
            }


            // Start the game
            StartGame();


        }


        // start game 

        public void StartGame()
        {

            //Reset the game Background
            ImageBrush nouveauBackground = new ImageBrush();
            nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/1_Back_Hollow.png"));
            nouveauBackground.Stretch = Stretch.UniformToFill;
            MainGrid.Background = nouveauBackground;

            // Reset the buttons
            foreach (Button btn in Grd_Keypad.Children.OfType<Button>())
            {
                btn.IsEnabled = true;
                
            }

            // Reset the display
            TB_Compte.Text = "8";


            // Reset the game state timer
            montimer.Interval = new TimeSpan(0, 0, 1);
            montimer.Tick += new EventHandler(timer_tick);
            montimer.Start();


            // Reset the game state
            motAleatoire = mots[random.Next(mots.Count)];


            lenght = motAleatoire.Length;


            // Reset the display
            TB_Display.Text = "";


            // Display underscores for each letter in the word
            for (int i = 0; i < lenght; i++)
            {
                TB_Display.Text += "_ ";
            }
        }

        void timer_tick(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }


        // Event handler for button clicks
        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            // Button is not enable
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
            btn.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));


            // Check if the letter is in the word , prog central
            if (motAleatoire.Contains(btnContent))
            {
                if (partieTerminee)
                    return;
                for (int i = 0; i < lenght; i++)
                {

                    // Button is correct
                    if (motAleatoire[i].ToString() == btnContent)
                    {
                        TB_Display.Text = TB_Display.Text.Remove(i * 2, 1);
                        TB_Display.Text = TB_Display.Text.Insert(i * 2, btnContent);

                        //Color Button correct
                        btn.Background = new SolidColorBrush(Color.FromRgb(130, 50, 190));
                    }
                }
            }

            // Nombre d'erreurs et met à jour l'affichage
            else
            {
                TB_Compte.Text = (Int32.Parse(TB_Compte.Text) - 1).ToString();
                UpdateBackground();
            }



            // Check if the player has won
            if (TB_Display.Text.Replace(" ", "") == motAleatoire)
            {

                // Play the winning sound
                MediaPlayer playMamusique = new MediaPlayer();
                var uri = new Uri(@"Resource/Sound/wow.mp3", UriKind.Relative);
                playMamusique.Open(uri);
                playMamusique.Play();

                MessageBox.Show("Vous avez gagné ! voulez vous rejouer?");

                partieTerminee = true;
                Application.Current.Shutdown();

            }
        }


        // Check if the player has lost 1 life
        public void UpdateBackground()
        {
            ImageBrush nouveauBackground = new ImageBrush();


            switch (TB_Compte.Text)
            {
                case "7":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/1_Back_Hollow.png"));
                    break;
                case "6":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/1_Back_Hollow.png"));
                    break;


                case "5":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/2_Back_Hollow.png"));
                    break;
                case "4":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/2_Back_Hollow.png"));
                    break;


                case "3":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/3_Back_Hollow.png"));
                    break;
                case "2":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/3_Back_Hollow.png"));
                    break;


                case "1":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/4_Back_Hollow.png"));
                    break;
                case "0":
                    nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/5_Back_Hollow.png"));
                    nouveauBackground.Stretch = Stretch.UniformToFill;
                    MainGrid.Background = nouveauBackground;

                    // Play the losing sound
                    MediaPlayer playMamusique = new MediaPlayer();
                    var uri = new Uri(@"Resource/Sound/spongebob-fail.mp3", UriKind.Relative);
                    playMamusique.Open(uri);
                    playMamusique.Play();
                    partieTerminee = true;
                    MessageBox.Show("Vous avez perdu ! Le mot était : " + motAleatoire);

                    break;
            }
            nouveauBackground.Stretch = Stretch.UniformToFill;
            MainGrid.Background = nouveauBackground;


        } 


        private void BTN_Relaunch_Click(object sender, RoutedEventArgs e)
        {
            // Color button reset
            foreach (Button btn in Grd_Keypad.Children.OfType<Button>())
            {
                btn.ClearValue(Button.BackgroundProperty);
                btn.ClearValue(Button.ForegroundProperty);
            }
                
            StartGame();
        }

        private void BTN_Leave_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
