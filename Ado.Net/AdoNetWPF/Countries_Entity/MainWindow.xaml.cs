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
        private Country_Entities db;

        public MainWindow()
        {
            InitializeComponent();
            db = new Country_Entities();
        }


        #region CRUD
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowForAdd wa = new WindowForAdd();

            wa.Title = $"Добавление в {"Countries"}";

            if (wa.ShowDialog() == true)
            {
                Countries country = wa.FillRow();
                if (country != null)
                {
                    db.Countries.Add(country);
                    db.SaveChanges();
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = db.Countries.OrderBy(c => c.id).ToList();
                }
            }
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Countries country = dataGrid.SelectedItem as Countries;
            if (country == null) return;
            WindowForAdd wa = new WindowForAdd(ref country);
           
            if (wa.ShowDialog() == true)
            {
                wa.UpdateCountry(ref country);
                db.SaveChanges();
                dataGrid.ItemsSource = null;
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

