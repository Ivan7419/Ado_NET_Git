using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using Dapper;


namespace DistributionList_Dapper
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["DistributionListConnection"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT * FROM Customers as C JOIN Cities AS Ct ON Ct.Id = C.CityId ").Select(c => new {c.FullName, c.Email, c.Gender, City = c.Name}).ToList();
                    }
                    break;
                case 1:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT Email FROM Customers").Select(c => new { c.Email }).ToList();
                    }
                    break;
                case 2:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT * FROM Sections").Select(s => new {s.Name }).ToList();
                    }
                    break;
                case 3:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT * FROM Discounts").Select(c => new { c.Name }).ToList();
                    }
                    break;
                case 4:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT * FROM Cities").Select(c => new { c.Name }).ToList();
                    }
                    break;
                case 5:
                    using (IDbConnection db = new SqlConnection(_connectionString))
                    {
                        dataGrid.ItemsSource = db.Query("SELECT * FROM Countries").Select(c => new { c.Name }).ToList();
                    }
                    break;
            }
        }
    }
}
