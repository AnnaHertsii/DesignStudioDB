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

        private void FindOrderButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowOrdersByOption(DataGridOrder, combobox_option, search_text);
        }

        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowOrders();
        }
    }
}
