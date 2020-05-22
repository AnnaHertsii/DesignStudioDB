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
using DesignStudioCoursework.Structure;

namespace DesignStudioCoursework.Review.ClientsReview
{
    public partial class UpdateClientWindow : Window
    {
        public UpdateClientWindow(int index)
        {
            InitializeComponent();
            fillClientFields(index);
        }

        public void fillClientFields(int index)
        {
            using (var Content = new DesignStudioEntities())
            {
                Client chosenClient = (from customer in Content.Customer
                                where customer.Customer_ID == index
                                select new Client
                                {
                                    Name = customer.Name,
                                    Phone = customer.Phone,
                                    Adress = customer.Adress,
                                    Mail_adress = customer.Mail_adress,
                                    Customer_type_id = (int)customer.Customer_type_Ref
                                }).FirstOrDefault();
                name.Text = chosenClient.Name;
                phone.Text = chosenClient.Phone;
                adress.Text = chosenClient.Adress;
                mail_adress.Text = chosenClient.Mail_adress;
            }           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
