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
            var designStudioEntities = new DesignStudioEntities();
            DataGridCustomer.ItemsSource = designStudioEntities.Customer.ToList().Select(c => new { c.Customer_ID, c.Name, c.Phone, c.Adress, c.Mail_adress});
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
