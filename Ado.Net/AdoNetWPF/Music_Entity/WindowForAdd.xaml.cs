using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Music_Entity;


namespace DZ_03_StationeryFirm
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class WindowForAdd : Window
    {
        public WindowForAdd()
        {
            InitializeComponent();
        }

        public Track FillRow()
        {
            int a;
            DateTime b;
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbAuthor.Text) ||
                !DateTime.TryParse(tbRD.Text, out b) ||
                !Int32.TryParse(tbDuration.Text, out a))
            {
                MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            Track track = new Track()
            {
                Name = tbName.Text,
                Author = tbAuthor.Text,
                Duration = Int32.Parse(tbDuration.Text),
                ReleaseDate = DateTime.Parse(tbRD.Text)
            };

            return track;
        }

        public WindowForAdd(ref Track track)
        {
            InitializeComponent();
            Title = $"Изменение в {"Music"}";
            tbName.Text = track.Name;
            tbAuthor.Text = track.Author;
            tbDuration.Text = track.Duration.ToString();
            tbRD.Text = track.ReleaseDate.ToShortDateString();
        }

        public void UpdateTrack(ref Track track)
        {
            int a;
            DateTime b;
            if (string.IsNullOrWhiteSpace(tbName.Text) ||
                string.IsNullOrWhiteSpace(tbAuthor.Text) ||
                !DateTime.TryParse(tbRD.Text, out b) ||
                !Int32.TryParse(tbDuration.Text, out a))
            {
                MessageBox.Show("Неккоректный ввод!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            track.Name = tbName.Text;
            track.Author = tbAuthor.Text;
            track.Duration = Int32.Parse(tbDuration.Text);
            track.ReleaseDate = DateTime.Parse(tbRD.Text);
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
