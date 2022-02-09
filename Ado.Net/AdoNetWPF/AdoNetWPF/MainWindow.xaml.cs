using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DZ_03_StationeryFirm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ConnectionString = "Data Source=I7-4700;Initial Catalog=StationeryFirm;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private DataTable MainTable = new DataTable();
        private SqlDataAdapter adapter;


        private delegate void AsyncAdapter(SqlDataAdapter adapterToUpdate, DataTable tableToUpdate);
        private AsyncAdapter updateAdapter;
        private AsyncAdapter fillAdapter;

        private Action showLoadImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connection = new SqlConnection(ConnectionString);
            command = connection.CreateCommand();
            adapter = new SqlDataAdapter("select * from types", ConnectionString);
            TryFillComboBoxTableNames();

            showLoadImage = () =>
            {
                ImageLoad.Visibility = Visibility.Visible;
            };

            updateAdapter = new AsyncAdapter((a, t) =>
            {
                Dispatcher.Invoke(showLoadImage);
                a.Update(t);
            });

            fillAdapter = new AsyncAdapter((a, t) =>
            {
                Dispatcher.Invoke(showLoadImage);
          
                t.Clear();
                a.Fill(t);
            });

            ListTableName.SelectedIndex = 0;
        }


        #region Выбор таблицы

        private void TryFillComboBoxTableNames()
        {
            string sqlTale = "select [name] from sys.tables where [name] <> 'sysdiagrams' order by [Name] desc;";

            try
            {
                connection.Open();
                command.CommandText = sqlTale;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        ListTableName.Items.Add(reader[i]);
                    }
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
        }

        private void ListTableName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBox)sender).SelectedItem.ToString() == "Sales" || ((ComboBox)sender).SelectedItem.ToString().StartsWith("Archieve")) // в тз не сказанно 
            {
                ButtonAddRecord.IsEnabled = false;
                ButtoDeleteRecord.IsEnabled = false;
                ButtonEditRecord.IsEnabled = false;
            }
            else
            {
                ButtonAddRecord.IsEnabled = true;
                ButtoDeleteRecord.IsEnabled = true;
                ButtonEditRecord.IsEnabled = true;
            }

            fillAdapter.BeginInvoke(adapter, MainTable, AsyncCbFill, null);

            ButtonAddRecord.Content = "Добавление в " + ((ComboBox)sender).SelectedItem;
        }

        #endregion


        #region CRUD
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (MainTable == null) return;

            WindowForAdd wa = new WindowForAdd(ListTableName.Text);

            wa.Title = $"Добавление в {ListTableName.Text}";

            if (wa.ShowDialog() == true)
            {
                DataRow row = FillRow(wa, true);

                MainTable.Rows.Add(row);
                UpdateTable();
            }
        }

        private DataRow FillRow(WindowForAdd wa, bool isAppend)
        {
            DataRow row = null;
            if (isAppend)
            {
                row = MainTable.NewRow();
            }
            else
            {
                row = MainTable.Rows[dataGrid.SelectedIndex];
            }

            string tableName = ListTableName.SelectedItem.ToString();

            if (tableName == "Types") row["Name"] = wa.NameTypeOrFirm.Text;
            else if (tableName == "Firms") row["Firm"] = wa.NameTypeOrFirm.Text;
            else if (tableName == "Managers" || tableName == "Customers")
            {
                row["FirstName"] = wa.FirstNamePerson.Text;
                row["LastName"] = wa.LastNamePerson.Text;
                row["Email"] = wa.EmailPerson.Text;
                row["Phone"] = wa.PhonePerson.Text;
            }
            else if (tableName == "Products")
            {
                row["Name"] = wa.NameProduct.Text;
                row["TypeId"] = TrySelectFind($"select Id From Types Where Name = '{wa.TypeIdProduct.Text}'");
                row["Count"] = wa.CountProduct.Text;
                row["Price"] = wa.PriceProduct.Text;
                row["FirmId"] = TrySelectFind($"select Id From Firms Where Firm = '{wa.FirmIdProduct.Text}'");
            }

            return row;
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (MainTable == null) return;

            WindowForAdd wa = new WindowForAdd(ListTableName.Text);

            wa.Title = $"Изменение в {ListTableName.Text}";

            if(!FillWindow(wa))return;

            if (wa.ShowDialog() == true)
            {
                DataRow row = FillRow(wa, false);
            }
            UpdateTable();
        }

        private bool FillWindow(WindowForAdd wa)
        {
            string cellValue = GetSelectedCellValue();
            if (cellValue == null) return false;

            string tableName = ListTableName.SelectedItem.ToString();

            if (tableName == "Types") wa.NameTypeOrFirm.Text = TrySelectFind($"Select Name From {ListTableName.Text} Where Id = {cellValue}");
            else if (tableName == "Firms") wa.NameTypeOrFirm.Text = TrySelectFind($"Select Firm From {ListTableName.Text} Where Id = {cellValue}");
            else if (tableName == "Managers" || tableName == "Customers")
            {
                wa.FirstNamePerson.Text = TrySelectFind($"Select F.FirstName From {ListTableName.Text} F Where F.Id = {cellValue}");
                wa.LastNamePerson.Text = TrySelectFind($"Select F.LastName From {ListTableName.Text} F Where F.Id = {cellValue}");
                wa.EmailPerson.Text = TrySelectFind($"Select F.Email From {ListTableName.Text} F Where F.Id = {cellValue}");
                wa.PhonePerson.Text = TrySelectFind($"Select F.Phone From {ListTableName.Text} F Where F.Id = {cellValue}");

            }
            else if (tableName == "Products")
            {
                wa.NameProduct.Text = TrySelectFind($"Select F.Name From {ListTableName.Text} F Where F.Id = {cellValue}");
                wa.CountProduct.Text = TrySelectFind($"Select F.Count From {ListTableName.Text} F Where F.Id = {cellValue}");
                wa.PriceProduct.Text = TrySelectFind($"Select F.Price From {ListTableName.Text} F Where F.Id = {cellValue}");
            }

            return true;
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Удалить запись?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)return;
            string cellValue = GetSelectedCellValue();
            if (cellValue == null) return;

            string sql = $"delete {ListTableName.Text} where Id = {cellValue}";

            try
            {
                connection.Open();
                adapter.DeleteCommand = connection.CreateCommand();
                adapter.DeleteCommand.CommandText = sql;
                adapter.DeleteCommand.ExecuteNonQuery();
                UpdateTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
        }

        #endregion


        #region Асинхронность

        private void AsyncCbUpdate(IAsyncResult ar)
        {
            updateAdapter.EndInvoke(ar);
            fillAdapter.BeginInvoke(adapter, MainTable, AsyncCbFill, null);
        }

        private void UpdateTable()
        {
            updateAdapter.BeginInvoke(adapter, MainTable, AsyncCbUpdate, null);
        }

        private void AsyncCbFill(IAsyncResult ar)
        {
            fillAdapter.EndInvoke(ar);

            Action a = () =>
            {
                MainTable = new DataTable();

                string sql = "WAITFOR DELAY '00:00:01'; " + "select * from " + ListTableName.SelectedItem;
                adapter = new SqlDataAdapter(sql, ConnectionString);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                adapter.Fill(MainTable);
                dataGrid.ItemsSource = MainTable.DefaultView;

                ImageLoad.Visibility = Visibility.Collapsed;
            };

            if (!CheckAccess())
            {
                Dispatcher.Invoke(a);
            }
            else
            {
                a();
            }

        }
        #endregion

        private string GetSelectedCellValue(int columnIndex = 0)
        {
            try
            {
                DataGridCellInfo cellInfo = dataGrid.SelectedCells[columnIndex];
                DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;

                if (column == null) return null;

                FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
                BindingOperations.SetBinding(element, TagProperty, column.Binding);

                return element.Tag.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Выберите значение!!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        private string TrySelectFind(string sql)
        {
            string data;
            try
            {
                connection.Open();
                command.CommandText = sql;

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                data = reader[0].ToString();

                reader.Close();
                connection.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                throw;
            }
            return data;
        }
    }
}
