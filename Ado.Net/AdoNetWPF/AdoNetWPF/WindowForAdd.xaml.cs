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

            if (typeTable == "Types" || typeTable == "Firms")
            {
                AddTypeOrFirm.Visibility = Visibility.Visible;
            }
            else if (typeTable == "Managers" || typeTable == "Customers")
            {
                AddPerson.Visibility = Visibility.Visible;
            }
            else if (typeTable == "Products")
            {
                AddProduct.Visibility = Visibility.Visible;
                FillComboBox(TypeIdProduct, $"select [Name] from Types order by [Name]");
                FillComboBox(FirmIdProduct, "select Firm from Firms");
            }
        }

        private void FillComboBox(ComboBox comboBox, string sql)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand command = connection.CreateCommand();

            connection.Open();
            command.CommandText = sql;
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    comboBox.Items.Add(reader[i]);
                }
            }

            reader.Close();
            connection.Close();
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
