using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DesignStudioCoursework.Structure;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace DesignStudioCoursework.Review.OrdersReview
{
    public partial class OrdersPage : Page
    {
        private Action goBack;
        SearchOrder search = new SearchOrder();

        public OrdersPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
        }

        public void ShowOrders()
        {
            using (var db = new DesignStudioEntities())
            {
                var orders = from order in db.Order
                                join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                                join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                select new
                                {
                                    order.Description,
                                    Start = order.Start_date,
                                    End = order.End_date,
                                    Price = order.Total_price,
                                    Customer = customer.Name,
                                    Employee = employee.Name,
                                    Position = position.Position_name
                                };
                DataGridOrder.ItemsSource = orders.ToList();
            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void FindOrderButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowOrdersByOption(DataGridOrder, combobox_option, search_text, datebox);
        }

        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOrders();
        }

        private void DeleteOrderButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteOrder();
            ShowOrders();
        }

        public void DeleteOrder()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int SelectedId = CurrentID();
            string strSQL = string.Format("DELETE [Order] WHERE Order_ID = '{0}'", SelectedId);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Замовлення видалено!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridOrder.SelectedCells[index];
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
            string Description = GetSelectedCellValue(0);
            string Customer = GetSelectedCellValue(4);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Order_ID FROM [Order] WHERE Description = '{0}' ORDER BY Order_ID DESC", Description);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

       

        private void combobox_option_DropDownClosed(object sender, EventArgs e)
        {
            if (combobox_option.SelectedIndex == 0)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 1)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 2)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 3)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 4)
            {
                datebox.Visibility = System.Windows.Visibility.Visible;
            }
            else if (combobox_option.SelectedIndex == 5)
            {
                datebox.Visibility = System.Windows.Visibility.Visible;
            }
        }            
    }
}
