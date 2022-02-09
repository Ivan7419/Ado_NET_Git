using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using DZ_03_StationeryFirm;

namespace Countries_Entity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Countries_Entities db;

        public MainWindow()
        {
            InitializeComponent();
            db = new Countries_Entities();
        }


        #region CRUD
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            WindowForAdd wa = new WindowForAdd("Countries");

            wa.Title = $"Добавление в {"Countries"}";

            if (wa.ShowDialog() == true)
            {
                if (FillRow(wa) != null) dataGrid.ItemsSource = db.Countries.OrderBy(c => c.id).ToList();
            }
        }

        private Countries FillRow(WindowForAdd wa)
        {
            int a;
            if (string.IsNullOrWhiteSpace(wa.tbName.Text) ||
                string.IsNullOrWhiteSpace(wa.tbCapital.Text) ||
                string.IsNullOrWhiteSpace(wa.tbContinent.Text) ||
                string.IsNullOrWhiteSpace(wa.tbPopulation.Text) ||
                !Int32.TryParse(wa.tbPopulation.Text, out a))
            {
                MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Countries country = new Countries()
            {
                CountryName = wa.tbName.Text,
                Capital = wa.tbCapital.Text,
                Population = wa.tbPopulation.Text,
                Area = Int32.Parse(wa.tbArea.Text),
                Continent = wa.tbContinent.Text
            };

            db.Countries.Add(country);
            db.SaveChanges();

            return country;
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Countries country = dataGrid.SelectedItem as Countries;
            if (country == null) return;
            WindowForAdd wa = new WindowForAdd("Countries");
            wa.Title = $"Изменение в {"Countries"}";
            wa.tbName.Text = country.CountryName;
            wa.tbCapital.Text = country.Capital;
            wa.tbPopulation.Text = country.Population;
            wa.tbArea.Text = country.Area.ToString();
            wa.tbContinent.Text = country.Continent;

            if (wa.ShowDialog() == true)
            {
                int a;
                if (string.IsNullOrWhiteSpace(wa.tbName.Text) ||
                    string.IsNullOrWhiteSpace(wa.tbCapital.Text) ||
                    string.IsNullOrWhiteSpace(wa.tbContinent.Text) ||
                    string.IsNullOrWhiteSpace(wa.tbPopulation.Text) ||
                    !Int32.TryParse(wa.tbPopulation.Text, out a))
                {
                    MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                country.CountryName = wa.tbName.Text;
                country.Capital = wa.tbCapital.Text;
                country.Population = wa.tbPopulation.Text;
                country.Area = Int32.Parse(wa.tbArea.Text);
                country.Continent = wa.tbContinent.Text;

                db.SaveChanges();
                dataGrid.ItemsSource = db.Countries.OrderBy(c => c.id).ToList();
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Countries country = dataGrid.SelectedItem as Countries;
            if (country != null)
            {
                if (MessageBox.Show("Удалить запись?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                db.Countries.Remove(country);
                db.SaveChanges();
                dataGrid.ItemsSource = db.Countries.OrderBy(c => c.id).ToList();
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = db.Countries.OrderBy(c => c.id).ToList();
        }
    }
}

