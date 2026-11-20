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



        public MainWindow()
        {
            InitializeComponent();


            DispatcherTimer montimer;
            montimer = new DispatcherTimer();
            montimer.Interval = new TimeSpan(0, 0, 1);
            montimer.Tick += new EventHandler(timer_tick);
            montimer.Start();

            void timer_tick(object sender, EventArgs e)
            {
                CommandManager.InvalidateRequerySuggested();
            }


            string chemin = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resource", "List", "Hollow_Mots.txt");

            if (!File.Exists(chemin))
            {
                MessageBox.Show("Le fichier Hollow_Mots.txt est introuvable !");
                Application.Current.Shutdown();
                return;
            }


            mots = File.ReadAllLines(chemin)
               .Where(m => !string.IsNullOrWhiteSpace(m))
               .Select(m => m.Trim().ToUpper())
               .ToList();



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

            motAleatoire = mots[random.Next(mots.Count)];


            lenght = motAleatoire.Length;


            for (int i = 0; i < lenght; i++)
            {
                TB_Display.Text += "_ ";
            }
        }


        private void BTN_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();

            btn.IsEnabled = false;

            if (motAleatoire.Contains(btnContent))
            {
                if (partieTerminee)
                    return;
                for (int i = 0; i < lenght; i++)
                {
                    if (motAleatoire[i].ToString() == btnContent)
                    {
                        TB_Display.Text = TB_Display.Text.Remove(i * 2, 1);
                        TB_Display.Text = TB_Display.Text.Insert(i * 2, btnContent);
                    }
                }
            }
            else
            {
                TB_Compte.Text = (Int32.Parse(TB_Compte.Text) - 1).ToString();
            }


            if (TB_Display.Text.Replace(" ", "") == motAleatoire)
            {
                MessageBox.Show("Vous avez gagné !");

                MediaPlayer playMamusique = new MediaPlayer();
                var uri = new Uri(@"Resource/Sound/wow.mp3", UriKind.Relative);
                playMamusique.Open(uri);
                playMamusique.Play();

                if (!File.Exists(@"Resource/Sound/wow.mp3"))
                {
                    MessageBox.Show("Le fichier son est introuvable !");
                    Application.Current.Shutdown();
                    return;
                }


                Application.Current.Shutdown();

                partieTerminee = true;
            }
            if (TB_Compte.Text == "5")
            {
                ImageBrush nouveauBackground = new ImageBrush();

                nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/2_Back_Hollow.png"));

                nouveauBackground.Stretch = Stretch.UniformToFill;

                MainGrid.Background = nouveauBackground;

            }

            if (TB_Compte.Text == "3")
            {
                ImageBrush nouveauBackground = new ImageBrush();

                nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/3_Back_Hollow.png"));

                nouveauBackground.Stretch = Stretch.UniformToFill;

                MainGrid.Background = nouveauBackground;


            }

            if (TB_Compte.Text == "1")
            {
                ImageBrush nouveauBackground = new ImageBrush();

                nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/4_Back_Hollow.png"));

                nouveauBackground.Stretch = Stretch.UniformToFill;

                MainGrid.Background = nouveauBackground;


            }


            if (TB_Compte.Text == "0")
            {
                ImageBrush nouveauBackground = new ImageBrush();

                nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/5_Back_Hollow.png"));

                nouveauBackground.Stretch = Stretch.UniformToFill;

                MainGrid.Background = nouveauBackground;


                MediaPlayer playMamusique = new MediaPlayer();
                var uri = new Uri(@"Resource/Sound/spongebob-fail.mp3",UriKind.Relative);
                playMamusique.Open(uri);
                playMamusique.Play();

                if (!File.Exists(@"Resource/Sound/spongebob-fail.mp3"))
                {
                    MessageBox.Show("Le fichier son est introuvable !");
                    Application.Current.Shutdown();
                    return;
                }


                partieTerminee = true;

                MessageBox.Show("Vous avez perdu ! Le mot était : " + motAleatoire);
            }
        }   


        private void BTN_Compte_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
        }

        private void BTN_Relaunch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
