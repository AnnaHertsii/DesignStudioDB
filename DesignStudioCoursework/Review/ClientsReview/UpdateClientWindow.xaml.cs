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
using DesignStudioCoursework.Structure;

namespace DesignStudioCoursework.Review.ClientsReview
{
    public partial class UpdateClientWindow : Window
    {
        public int customer_index;
        DataGrid datagrid;
        DisplayClient display = new DisplayClient();

        public UpdateClientWindow(int index, DataGrid grid_name)
        {
            InitializeComponent();
            customer_index = index;
            datagrid = grid_name;
            fillClientFields();           
        }

        public void fillClientFields()
        {
            using (var db = new DesignStudioEntities())
            {
                Client chosenClient = (from customer in db.Customer
                                where customer.Customer_ID == customer_index
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

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {          
            UpdateCustomer();
            display.ShowCustomers(datagrid);
        }

        public void UpdateCustomer()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();

            int custom_type = 1;
            if (customer_type.Text == "Приватний клієнт")
                custom_type = 1;
            else if (customer_type.Text == "Компанія")
                custom_type = 2;

            string strSQL = string.Format("Update Customer Set Customer_ID = '{0}', Name ='{1}', Phone = '{2}', Adress = '{3}', Mail_adress = '{4}', Customer_type_Ref = '{5}'" +
                " Where Customer_ID = '{6}'", customer_index, name.Text, phone.Text, adress.Text, mail_adress.Text, custom_type, customer_index);

            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Дані клієнта успішно оновлено!");           
            this.Close();
        }
    }
}
