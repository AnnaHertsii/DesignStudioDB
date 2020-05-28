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

        private void end_date_click(object sender, RoutedEventArgs e)
        {           
            end_date.SelectedDate = start_date.SelectedDate;
            end_date.DisplayDateStart = start_date.SelectedDate;
        }

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (description.Text.Length > 150)
                description_error.Visibility = Visibility.Visible;
            else
            {
                description_error.Visibility = Visibility.Hidden;
            }

            if (end_date.SelectedDate < start_date.SelectedDate)
            {
                enddate_error.Visibility = Visibility.Visible;
            }
            else
            {
                enddate_error.Visibility = Visibility.Hidden;
            }

            bool isDigit = true;
            foreach (char c in price.Text)
            {
                if (c < '0' || c > '9')
                    isDigit = false;
            }
            if (isDigit == false)
            {
                price_error.Visibility = Visibility.Visible;
            }
            else
            {
                price_error.Visibility = Visibility.Hidden;
            }

            if (customercombo.SelectedIndex == -1)
                customer_error.Visibility = Visibility.Visible;
            else
            {
                customer_error.Visibility = Visibility.Hidden;
            }

            if (employeecombo.SelectedIndex == -1)
                employee_error.Visibility = Visibility.Visible;
            else
            {
                employee_error.Visibility = Visibility.Hidden;
            }
            
            if ((description.Text.Length < 150) && (end_date.SelectedDate >= start_date.SelectedDate) && (isDigit == true) && (customercombo.SelectedIndex != -1) && (employeecombo.SelectedIndex != -1))
            {
                description_error.Visibility = Visibility.Hidden;
                enddate_error.Visibility = Visibility.Hidden;
                price_error.Visibility = Visibility.Hidden;
                customer_error.Visibility = Visibility.Hidden;
                employee_error.Visibility = Visibility.Hidden;
                AddOrder();
            }           
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
                int totalprice = 0;
                if (price.Text != "")
                {
                    totalprice = Int32.Parse(price.Text);
                }

                string formattedstart = null;
                string formattedend = null;
                DateTime? startDate = start_date.SelectedDate;
                DateTime? endDate = end_date.SelectedDate;
                if (startDate.HasValue && endDate.HasValue)
                {
                    formattedstart = startDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    formattedend = endDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }

                string strSQL = string.Format("INSERT INTO [Order](Order_ID, Description, Start_date, End_date, Total_price, Customer_Ref, Employee_Ref) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')",
                    order_id, description.Text, formattedstart, formattedend, totalprice, Customer_id, Employee_id);

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
