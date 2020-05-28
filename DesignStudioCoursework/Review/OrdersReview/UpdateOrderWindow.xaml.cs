using DesignStudioCoursework.Structure;
using System;
using System.Collections.Generic;
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
    /// <summary>
    /// Логика взаимодействия для UpdateOrderWindow.xaml
    /// </summary>
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
                price.Text = chosenOrder.Price.ToString();
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
    }
}
