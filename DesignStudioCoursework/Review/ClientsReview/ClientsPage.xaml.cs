using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DesignStudioCoursework.Structure;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.Review.ClientsReview
{
    public partial class ClientsPage : Page
    {
        private Action goBack;
        SearchClient search = new SearchClient();
        DisplayClient display = new DisplayClient();

        public ClientsPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
        } 

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();       
        }

        public void UpdateCustomer()
        {
            int index = CurrentID();
            UpdateClientWindow updateClient = new UpdateClientWindow(index, DataGridCustomer);
            updateClient.Show();
        }

        private void ShowCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            display.ShowCustomers(DataGridCustomer);
        }
      
        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteCustomer();
            display.ShowCustomers(DataGridCustomer);
        }

        public void DeleteCustomer()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int SelectedId = CurrentID();
            string strSQL = string.Format("DELETE Customer WHERE Customer_ID = '{0}'", SelectedId);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Клієнта видалено!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridCustomer.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(0);
            string Phone = GetSelectedCellValue(1);
            string Adress = GetSelectedCellValue(2);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Customer_ID FROM Customer WHERE Name = '{0}' AND Phone = '{1}' AND Adress = '{2}'", Name, Phone, Adress);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        private void FindCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowClientsByOption(DataGridCustomer, combobox_option, search_text);
        }
    }
}
