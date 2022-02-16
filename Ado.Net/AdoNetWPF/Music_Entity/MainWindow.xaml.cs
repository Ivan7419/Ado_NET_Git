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
using DZ_03_StationeryFirm;

namespace Music_Entity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Music_DB db;
        public MainWindow()
        {
            InitializeComponent();
            db = new Music_DB();
        }

        #region CRUD
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowForAdd wa = new WindowForAdd();

            wa.Title = "Добавление в Music";

            if (wa.ShowDialog() == true)
            {
                Track tmp = wa.FillRow();
                if (tmp != null)
                {
                    db.Tracks.Add(tmp);
                    db.SaveChanges();
                    dataGrid.ItemsSource = null;
                    dataGrid.ItemsSource = db.Tracks.OrderBy(c => c.Id).ToList();
                }
            }
        }


        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            Track track = dataGrid.SelectedItem as Track;
            if (track == null) return;
            WindowForAdd wa = new WindowForAdd(ref track);

            if (wa.ShowDialog() == true)
            {
                wa.UpdateTrack(ref track);
                db.SaveChanges();
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = db.Tracks.OrderBy(c => c.Id).ToList();
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Track track = dataGrid.SelectedItem as Track;
            if (track != null)
            {
                if (MessageBox.Show("Удалить запись?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                db.Tracks.Remove(track);
                db.SaveChanges();
                dataGrid.ItemsSource = db.Tracks.OrderBy(c => c.Id).ToList();
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = db.Tracks.OrderBy(t => t.Id).ToList();
        }
    }
}
