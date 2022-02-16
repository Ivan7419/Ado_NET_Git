using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Games_MF;


namespace DZ_03_StationeryFirm
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class WindowForAdd : Window
    {
        private bool IsGame;
        #region ADD RECORD
        public WindowForAdd(bool IsGame)
        {
            this.IsGame = IsGame;
            InitializeComponent();
            if (IsGame)
            {
                AddGame.Visibility = Visibility.Visible;
                FillComboBox();
            }
            else AddDeveloper.Visibility = Visibility.Visible;
        }

        public object FillRow()
        {
            if (!IsGame)
            {
                if (string.IsNullOrWhiteSpace(tbDeveloper.Text))
                {
                    MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                Developer developer = new Developer()
                {
                    Name = tbDeveloper.Text
                };

                return developer;
            }
            else
            {
                Games_DB db = new Games_DB();
                DateTime a;
                if (string.IsNullOrWhiteSpace(tbName.Text) ||
                    string.IsNullOrWhiteSpace(tbPlatform.Text) ||
                    string.IsNullOrWhiteSpace(tbDistributor.Text) ||
                    !DateTime.TryParse(tbReleaseDate.Text, out a) ||
                    cbDeveloper.SelectedItem != null)
                {
                    MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    return null;
                }

                Games game = new Games()
                {
                    Name = tbName.Text,
                    Platform = tbPlatform.Text,
                    Distributor = tbDistributor.Text,
                    ReleaseDate = DateTime.Parse(tbReleaseDate.Text),
                    Developer = cbDeveloper.SelectedItem as Developer
                };

                return game;
            }
        }


        void FillComboBox()
        {
            Games_DB db = new Games_DB();
            cbDeveloper.DisplayMemberPath = "Name";
            cbDeveloper.SelectedValuePath = "Id";
            foreach (var d in db.Developers)
            {
                cbDeveloper.Items.Add(d);
            }
        }
        #endregion

        #region UPDATE
        //public WindowForAdd(ref Track track)
        //{
        //    InitializeComponent();
        //    Title = $"Изменение в {"Music"}";
        //    tbName.Text = track.Name;
        //    tbAuthor.Text = track.Author;
        //    tbDuration.Text = track.Duration.ToString();
        //    tbRD.Text = track.ReleaseDate.ToShortDateString();
        //}

        //public void UpdateTrack(ref Track track)
        //{
        //    int a;
        //    DateTime b;
        //    if (string.IsNullOrWhiteSpace(tbName.Text) ||
        //        string.IsNullOrWhiteSpace(tbAuthor.Text) ||
        //        !DateTime.TryParse(tbRD.Text, out b) ||
        //        !Int32.TryParse(tbDuration.Text, out a))
        //    {
        //        MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return;
        //    }
        //    track.Name = tbName.Text;
        //    track.Author = tbAuthor.Text;
        //    track.Duration = Int32.Parse(tbDuration.Text);
        //    track.ReleaseDate = DateTime.Parse(tbRD.Text);
        //}
        #endregion


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
