using AppChef.Pages;
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

namespace AppChef
{
    /// <summary>
    /// Interaction logic for Interfata.xaml
    /// </summary>
    public partial class Interfata : Window
    {
        public Interfata()
        {
            InitializeComponent();
            Main.Content = new HomePage();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomePage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Main.Content = new PartiesPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Main.Content = new LocationsPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Main.Content = new AccountDetails();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.ShowDialog();
            this.Close();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}
