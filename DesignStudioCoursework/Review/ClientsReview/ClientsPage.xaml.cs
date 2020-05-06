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

namespace DesignStudioCoursework.Review.ClientsReview
{
    public partial class ClientsPage : Page
    {
        private Action goBack;

        public ClientsPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            ShowCustomers();
            //var designStudioEntities = new DesignStudioEntities();
            //DataGridCustomer.ItemsSource = designStudioEntities.Customer.ToList().Select(c => new { c.Customer_ID, c.Name, c.Phone, c.Adress, c.Mail_adress});
        }

        public void ShowCustomers()
        {
            using (var Content = new DesignStudioEntities())
            {
                var customers = from customer in Content.Customer
                                   join type in Content.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                   select new
                                   {
                                       customer.Customer_ID,
                                       customer.Name,
                                       customer.Phone,
                                       customer.Adress,
                                       customer.Mail_adress,
                                       type.Customer_type1
                                   };
                DataGridCustomer.ItemsSource = customers.ToList();
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
