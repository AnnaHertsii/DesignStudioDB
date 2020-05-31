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

namespace DesignStudioCoursework.NewProject
{
    public partial class OrdersWindow : Window
    {
        SearchOrder search = new SearchOrder();
        TextBox order;

        public OrdersWindow(TextBox order1)
        {
            InitializeComponent();
            ShowOrders();
            order = order1;
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

        private void FindOrderButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowOrdersByOption(DataGridOrder, combobox_option, search_text, datebox);
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

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOrders();
        }

        private void ChooseOrderButton_Click(object sender, RoutedEventArgs e)
        {
            string Description = GetSelectedCellValue(0);
            order.Text = Description;
            this.Close();
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
    }
}
