using DesignStudioCoursework.Structure;
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
using System.Windows.Shapes;

namespace DesignStudioCoursework.Review.OrdersReview
{
    public partial class UpdateOrderWindow : Window
    {   
        public List<Customer> Customers { get; set; }
        public int order_index;
        DataGrid datagrid;
        DisplayOrder display = new DisplayOrder();

        public UpdateOrderWindow(int index, DataGrid grid_name)
        {
            order_index = index;
            InitializeComponent();
            BindComboCustomer();
            datagrid = grid_name;
            fillOrderFields();
        }

        public void fillOrderFields()
        {
            using (var db = new DesignStudioEntities())
            {
                var chosenOrder = (from order in db.Order
                                       where order.Order_ID == order_index
                                       select new
                                       {
                                           Descriptiona = order.Description,
                                           Start = order.Start_date,
                                           End = order.End_date,
                                           Price = order.Total_price,
                                           Customer = order.Customer_Ref,
                                           Employee = order.Employee_Ref,
                                       }).FirstOrDefault();
                description.Text = chosenOrder.Descriptiona;
                start_date.SelectedDate = chosenOrder.Start;
                end_date.SelectedDate = chosenOrder.End;
                price.Text = Math.Truncate((decimal)chosenOrder.Price).ToString();
                customercombo.SelectedIndex = (int)chosenOrder.Customer-1;
                employeecombo.SelectedIndex = (int)chosenOrder.Employee - 1;
            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BindComboCustomer()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Customer.ToList();
            Customers = items;
            DataContext = Customers;
        }

        public void UpdateOrder()
        {
            try
            {
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

                string strSQL = string.Format("UPDATE [Order] SET Order_ID = '{0}', Description = '{1}', Start_date = '{2}', End_date = '{3}', Total_price = '{4}', Customer_Ref = '{5}', Employee_Ref = '{6}' WHERE Order_ID = '{7}'",
                    order_index, description.Text, formattedstart, formattedend, totalprice, Customer_id, Employee_id, order_index);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Замовлення успішно оновлено!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
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
                UpdateOrder();
            }
            display.ShowOrders(datagrid);
        }
    }
}
