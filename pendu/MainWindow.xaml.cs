using System;
using System.Collections.Generic;
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
using System.IO;

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
                Application.Current.Shutdown();

                partieTerminee = true;
            }
            if (TB_Compte.Text == "0")
            {
                ImageBrush nouveauBackground = new ImageBrush();

                nouveauBackground.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Resource/BackGround/2_Back_Hollow.png"));

                nouveauBackground.Stretch = Stretch.UniformToFill;

                MainGrid.Background = nouveauBackground;

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


    }
}
