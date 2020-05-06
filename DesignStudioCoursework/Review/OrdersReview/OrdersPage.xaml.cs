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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.Review.OrdersReview
{
    public partial class OrdersPage : Page
    {
        private Action goBack;

        public OrdersPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            ShowOrders();
        }

        public void ShowOrders()
        {
            using (var Content = new DesignStudioEntities())
            {
                var orders = from order in Content.Order
                                join customer in Content.Customer on order.Customer_Ref equals customer.Customer_ID
                                join employee in Content.Employee on order.Employee_Ref equals employee.Employee_ID
                                join position in Content.Position on employee.Position_Ref equals position.Position_ID
                                select new
                                {
                                    order.Order_ID,
                                    order.Description,
                                    order.Start_date,
                                    order.End_date,
                                    order.Total_price,
                                    customer.Name,
                                    //employee.Name,
                                    position.Position_name

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
    }
}
