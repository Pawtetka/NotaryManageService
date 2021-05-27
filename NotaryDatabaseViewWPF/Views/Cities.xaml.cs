using NotaryDatabaseDLL.Classes;
using NotaryDatabaseDLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NotaryDatabaseViewWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для Cities.xaml
    /// </summary>
    public partial class Cities : Window
    {
        private DbController dbController;
        public Cities(DbController dbController)
        {
            InitializeComponent();
            this.dbController = dbController;
        }

        private void Show_Cities_Button_Click(object sender, RoutedEventArgs e)
        {
            var cities = dbController.GetAllCities();
            ResultText.Text = "";
            foreach (City city in cities)
            {
                ResultText.Text += city.CityName + "\n";
            }
        }
        private void Add_City_Button_Click(object sender, RoutedEventArgs e)
        {
            if (City_Name_Text.Text != "" && City_Type_Text.Text != "")
            {
                dbController.AddNewCity(City_Name_Text.Text, City_Type_Text.Text);
                MessageBox.Show("Done!");
                City_Name_Text.Text = "";
                City_Type_Text.Text = "";
            }
            else
            {
                MessageBox.Show("Сначала заполните поля");
            }
        }

        private void Show_City_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            if (City_Name_Text.Text != "")
            {
                var cities = dbController.GetCity(City_Name_Text.Text);
                ResultText.Text = "";
                foreach (City city in cities)
                {
                    ResultText.Text += "City:\n" + city.CityId + "\n" + 
                        city.CityName + "\n" + city.CityType + "\n";
                }
                City_Name_Text.Text = "";
            }
            else
            {
                MessageBox.Show("Сначала заполните поле");
            }
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
