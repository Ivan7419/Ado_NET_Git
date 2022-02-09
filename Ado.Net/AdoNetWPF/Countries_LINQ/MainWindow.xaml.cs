using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Countries_LINQ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "Data Source=10.6.1.191;Initial Catalog=Onischenko;Persist Security Info=True;User ID=pv011;Password=147852";
        private DataContext db;
        private Countries countries;
        private bool IsInizalizated = false;

        public MainWindow()
        {
            InitializeComponent();
            IsInizalizated = true;
            db = new DataContext(ConnectionString);
            countries = new Countries();
            CbFirstTask_SelectionChanged(cbFirstTask, null);
        }

        private void RbTask_Checked(object sender, RoutedEventArgs e)
        {
            if (!IsInizalizated) return;
            dataGrid.ItemsSource = null;
            switch ((sender as RadioButton).Name)
            {
                case "rbFirstTask":
                    cbFirstTask.IsEnabled = true;
                    cbSecondTask.IsEnabled = false;
                    cbThirdTask.IsEnabled = false;
                    cbFirstTask.SelectedIndex = 0;
                    CbFirstTask_SelectionChanged(cbFirstTask, null);
                    break;
                case "rbSecondTask":
                    cbSecondTask.IsEnabled = true;
                    cbFirstTask.IsEnabled = false;
                    cbThirdTask.IsEnabled = false;
                    cbSecondTask.SelectedIndex = 0;
                    CbSecondTask_SelectionChanged(cbSecondTask, null);
                    break;
                case "rbThirdTask":
                    cbThirdTask.IsEnabled = true;
                    cbFirstTask.IsEnabled = false;
                    cbSecondTask.IsEnabled = false;
                    cbThirdTask.SelectedIndex = 0;
                    CbThirdTask_SelectionChanged(cbThirdTask, null);
                    break;
            }
        }

        private void CbFirstTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInizalizated) return;
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    dataGrid.ItemsSource = db.GetTable<CountryClass>();
                    break;
                case 1:
                    dataGrid.ItemsSource = from country in countries.Country select new { CountryName = country.Name };
                    break;
                case 2:
                    dataGrid.ItemsSource = from country in countries.Country select new { country.Capital };
                    break;
                case 3:
                    dataGrid.ItemsSource = from country in countries.Country
                                           where (country.Continent == "EU")
                                           select new { CountryName = country.Name };
                    break;
                case 4:
                    int a;
                    if (Int32.TryParse(tbScale.Text, out a))
                        dataGrid.ItemsSource = from country in countries.Country
                                               where (country.Area > Int32.Parse(tbScale.Text))
                                               select new { CountryName = country.Name };
                    else
                    {
                        MessageBox.Show("Неккоректный ввод", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        cbFirstTask.SelectedIndex = 0;
                    }

                    break;
            }
        }

        private void CbSecondTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    dataGrid.ItemsSource = from country in countries.Country where (country.Name.Contains("a") || Name.Contains("u")) select new { CountryName = country.Name };
                    break;
                case 1:
                    dataGrid.ItemsSource = from country in countries.Country where (country.Name.StartsWith("B")) select new { CountryName = country.Name };
                    break;
                case 2:
                    int a;
                    if (Int32.TryParse(tbScTO.Text, out a) && Int32.TryParse(tbScFr.Text, out a))
                        dataGrid.ItemsSource = from country in countries.Country where (country.Area >= Int32.Parse(tbScFr.Text) && country.Area <= Int32.Parse(tbScTO.Text)) select new { CountryName = country.Name };
                    else
                    {
                        MessageBox.Show("Неккоректный ввод", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        cbSecondTask.SelectedIndex = 0;
                    }
                    break;
                case 3:
                    if (Int32.TryParse(tbPeople.Text, out a))
                        dataGrid.ItemsSource = from country in countries.Country where (country.Population > Int32.Parse(tbPeople.Text)) select new { CountryName = country.Name };
                    else
                    {
                        MessageBox.Show("Неккоректный ввод", "ОШИБКА", MessageBoxButton.OK, MessageBoxImage.Error);
                        cbSecondTask.SelectedIndex = 0;
                    }
                    break;
            }
        }

        private void CbThirdTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInizalizated) return;
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    dataGrid.ItemsSource = (from country in countries.Country orderby country.Area descending select new { CountryName = country.Name }).Take(5);
                    break;
                case 1:
                    dataGrid.ItemsSource = (from country in countries.Country orderby country.Population descending select new { CountryName = country.Name }).Take(5);
                    break;
                case 2:
                    dataGrid.ItemsSource = (from country in countries.Country orderby country.Area descending select new { CountryName = country.Name }).Take(1);
                    break;
                case 3:
                    dataGrid.ItemsSource = (from country in countries.Country orderby country.Population descending select new { CountryName = country.Name }).Take(1);
                    break;
                case 4:
                    dataGrid.ItemsSource = (from country in countries.Country where (country.Continent == "EU") orderby country.Area descending select new { CountryName = country.Name }).Take(1);
                    break;
                case 5:
                    dataGrid.ItemsSource = (from country in countries.Country where (country.Continent == "EU") select new { СрПлощадь = countries.Country.Average(c => c.Area) }).Take(1);
                    break;
            }
        }
    }
}