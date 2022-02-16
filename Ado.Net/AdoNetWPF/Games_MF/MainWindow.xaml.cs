using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DZ_03_StationeryFirm;

namespace Games_MF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Games_DB db;
        private bool AddGame;

        public MainWindow()
        {
            InitializeComponent();
            db = new Games_DB();
        }

        #region CRUD
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            WindowForAdd wa = new WindowForAdd(AddGame);
            if (wa.ShowDialog() == true)
            {
                if (AddGame)
                {
                    Games tmp = wa.FillRow() as Games;
                    if (tmp != null)
                    {
                        db.GamesCollection.Add(tmp);
                        db.SaveChanges();
                        dataGridGames.ItemsSource = null;
                        //dataGridGames.ItemsSource = db.Games.OrderBy(c => c.Id).ToList();
                    }
                }
                else
                {
                    Developer tmp = (wa.FillRow() as Developer);
                    if (tmp != null)
                    {
                        db.Developers.Add(tmp);
                        db.SaveChanges();
                        dataGridDevelop.ItemsSource = null;
                        dataGridDevelop.ItemsSource = db.Developers.OrderBy(c => c.Id).ToList();
                        if(dataGridDevelop.Items.Count>0) dataGridDevelop.SelectedIndex = dataGridDevelop.Items.Count - 1;
                    }
                }
            }
        }


        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            //Track track = dataGrid.SelectedItem as Track;
            //if (track == null) return;
            //WindowForAdd wa = new WindowForAdd(ref track);

            //if (wa.ShowDialog() == true)
            //{
            //    wa.UpdateTrack(ref track);
            //    db.SaveChanges();
            //    dataGrid.ItemsSource = null;
            //    dataGrid.ItemsSource = db.Tracks.OrderBy(c => c.Id).ToList();
            //}
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridGames.SelectedIndex == -1)
            {
                Developer developer = (dataGridDevelop.SelectedItem as Developer);
                if (developer != null)
                {
                    if (MessageBox.Show("Удалить запись?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                    db.Developers.Remove(developer);
                    db.SaveChanges();
                    dataGridDevelop.ItemsSource = db.Developers.OrderBy(t => t.Id).ToList();
                    dataGridGames.Visibility = Visibility.Hidden;
                    if (dataGridDevelop.Items.Count > 0) dataGridDevelop.SelectedIndex = dataGridDevelop.Items.Count - 1;
                }
            }
            else
            {
                var tmp = (dataGridGames.SelectedItem as Games);
                Games game = new Games
                {
                    Name = tmp.Name,
                    Platform = tmp.Platform,
                    Distributor = tmp.Distributor,
                    Developer = tmp.Developer,
                    ReleaseDate = tmp.ReleaseDate
                };
                if (game != null)
                {
                    if (MessageBox.Show("Удалить запись?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) return;
                    db.GamesCollection.Remove(game);
                    db.SaveChanges();
                    DataGridDevelop_OnSelectionChanged(dataGridDevelop, null);
                }
            }
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGridGames.Visibility = Visibility.Hidden;
            dataGridDevelop.ItemsSource = db.Developers.OrderBy(t => t.Id).ToList();
            dataGridDevelop.SelectedIndex = 0;
        }

        private void DataGridDevelop_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridDevelop.SelectedItem == null) return;
            Developer selDeveloper = dataGridDevelop.SelectedItem as Developer;
            var tmp = db.GamesCollection.Where(g => g.Developer.Id == selDeveloper.Id).Select(s => new { s.Id, s.Name, s.Distributor, s.ReleaseDate, s.Platform }).OrderBy(g => g.Id).ToList();
            dataGridGames.ItemsSource = tmp;
            dataGridGames.Visibility = Visibility.Visible;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonAddRecord.IsEnabled = true;
            if ((cbOrder.SelectedItem as TextBlock).Text == "Game")
            {
                AddGame = true;
            }
            else
            {
                AddGame = false;
            }
        }
    }
}
