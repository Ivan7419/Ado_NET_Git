using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Countries_Entity;


namespace DZ_03_StationeryFirm
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class WindowForAdd: Window
    {
        public WindowForAdd()
        {
            InitializeComponent();
        }

        public Countries FillRow()
        {
            int a;
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbCapital.Text) ||
                string.IsNullOrWhiteSpace(tbContinent.Text) ||
                !Int32.TryParse(tbArea.Text, out a) ||
                !Int32.TryParse(tbPopulation.Text, out a))
            {
                MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Countries country = new Countries()
            {
                CountryName = tbName.Text,
                Capital = tbCapital.Text,
                Continent = tbContinent.Text,
                Population = Int32.Parse(tbPopulation.Text),
                Area = Int32.Parse(tbArea.Text)
            };

            return country;
        }

        public WindowForAdd(ref Countries country)
        {
            InitializeComponent();
            Title = $"Изменение в {"Countries"}";
            tbName.Text = country.CountryName;
            tbCapital.Text = country.Capital;
            tbContinent.Text = country.Continent.ToString();
            tbArea.Text = country.Area.ToString();
            tbPopulation.Text = country.Population.ToString();
        }

        public void UpdateCountry(ref Countries country)
        {
            int a;
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbCapital.Text) ||
                string.IsNullOrWhiteSpace(tbContinent.Text) ||
                !Int32.TryParse(tbArea.Text, out a) ||
                !Int32.TryParse(tbPopulation.Text, out a))
            {
                MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            country.CountryName = tbName.Text;
            country.Capital = tbCapital.Text;
            country.Continent = tbContinent.Text;
            country.Area = Int32.Parse(tbArea.Text);
            country.Population = Int32.Parse(tbPopulation.Text);

        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
