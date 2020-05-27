using System;
using System.Collections.Generic;
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

namespace DesignStudioCoursework
{
    public partial class NewOrderPage : Page
    {        
        public List<Customer> Customers { get; set; }
        public List<Employee> Employees { get; set; }
        private Action goBack;

        public NewOrderPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            BindComboCustomer();
        }
       
        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrder();
        }

        public void AddOrder()
        {
            try
            {
                int order_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                int Customer_id = customercombo.SelectedIndex + 1;
                int Employee_id = employeecombo.SelectedIndex + 1;

                DateTime startdate = start_date.SelectedDate.Value;
                DateTime enddate = end_date.SelectedDate.Value;
                int totalprice = Int32.Parse(price.Text);

                /*DateTime? selectedDate = start_date.SelectedDate;
                if (selectedDate.HasValue)
                {
                    string formatted = selectedDate.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }*/

                string strSQL = string.Format("INSERT INTO [Order](Order_ID, Description, Start_date, Total_price, Customer_Ref, Employee_Ref) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", order_id, description.Text, startdate, totalprice, Customer_id, Employee_id);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Нове замовлення успішно оформлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int MaxID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = "SELECT MAX(Order_ID) FROM [Order]";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
       
        private void BindComboCustomer()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Customer.ToList();
            Customers = items;
            DataContext = Customers;
        }
    }
}
