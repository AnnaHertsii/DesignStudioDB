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
        private Action goBack;

        public UpdateOrderWindow(Action goBack)
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

        private void BindComboCustomer()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Customer.ToList();
            Customers = items;
            DataContext = Customers;
        }
    }
}
