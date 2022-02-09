using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace DZ_03_StationeryFirm
{
    /// <summary>
    /// Логика взаимодействия для AddRecord.xaml
    /// </summary>
    public partial class WindowForAdd: Window
    {
        private const string ConnectionString = "Data Source=I7-4700;Initial Catalog=StationeryFirm;Integrated Security=True";

        public WindowForAdd(string typeTable)
        {
            InitializeComponent();
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
