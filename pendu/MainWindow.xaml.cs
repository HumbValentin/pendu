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

namespace pendu
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> mots = new List<string>()
        {
            "ORDINATEUR", "SOURIS", "CLAVIER", "ECRAN",
            "IMPRIMANTE", "TABLETTE", "TELEPHONE",
            "LOGICIEL", "APPLICATION", "RESEAU"
        };

        Random random = new Random();

        string motAleatoire;
        int lenght;



        public MainWindow()
        {
            InitializeComponent();

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
            }
            if (TB_Compte.Text == "0")
            {
                MessageBox.Show("Vous avez perdu ! Le mot était : " + motAleatoire);
                Application.Current.Shutdown();
            }
        }

        private void BTN_Valid_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Compte_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            String btnContent = btn.Content.ToString();
            btn.IsEnabled = false;
        }


    }
}
