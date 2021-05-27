using NotaryDatabaseDLL.Classes;
using NotaryDatabaseViewWPF.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NotaryDatabaseViewWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DbController dbController;
        private Cities cities;
        private Offices offices;
        private Workers workers;
        private Receptions receptions;
        public MainWindow()
        {
            InitializeComponent();
            dbController = new DbController();
        }

        private void Cities_Button_Click(object sender, RoutedEventArgs e)
        {
            cities = new Cities(dbController);
            cities.Show();
        }
        private void Offices_Button_Click(object sender, RoutedEventArgs e)
        {
            offices = new Offices(dbController);
            offices.Show();
        }
        private void Workers_Button_Click(object sender, RoutedEventArgs e)
        {
            workers = new Workers(dbController);
            workers.Show();
        }
        private void Notaries_Button_Click(object sender, RoutedEventArgs e)
        {
            cities = new Cities(dbController);
            cities.Show();
        }
        private void Locations_Button_Click(object sender, RoutedEventArgs e)
        {
            cities = new Cities(dbController);
            cities.Show();
        }
    }

}
