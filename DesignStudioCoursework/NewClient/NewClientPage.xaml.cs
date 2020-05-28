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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework
{
    public partial class NewClientPage : Page       
    {
        private Action goBack;

        public NewClientPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length > 150)
                name_error.Visibility = Visibility.Visible;
            else
            {
                name_error.Visibility = Visibility.Hidden;
            }
            if (phone.Text.Length > 15)
                phone_error.Visibility = Visibility.Visible;
            else
            {
                phone_error.Visibility = Visibility.Hidden;
            }
            if (adress.Text.Length > 50)
                adress_error.Visibility = Visibility.Visible;
            else
            {
                adress_error.Visibility = Visibility.Hidden;
            }
            if (mail_adress.Text.Length > 50)
                mail_error.Visibility = Visibility.Visible;
            else
            {
                mail_error.Visibility = Visibility.Hidden;
            }
            if ((name.Text.Length < 150) && (phone.Text.Length < 15) && (adress.Text.Length < 50) && (mail_adress.Text.Length < 15))
            {
                name_error.Visibility = Visibility.Hidden;
                phone_error.Visibility = Visibility.Hidden;
                adress_error.Visibility = Visibility.Hidden;
                mail_error.Visibility = Visibility.Hidden;
                AddCustomer();
            }               
        } 

        public void AddCustomer()
        {
            try
            {
                int customer_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();

                int custom_type = 1;
                if (customer_type.Text == "Приватний клієнт")
                    custom_type = 1;
                else if (customer_type.Text == "Компанія")
                    custom_type = 2;

                string strSQL = string.Format("INSERT INTO Customer(Customer_ID, Name, Phone, Adress, Mail_adress, Customer_type_Ref) " +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", customer_id, name.Text, phone.Text, adress.Text, mail_adress.Text, custom_type);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Новий клієнт успішно зареєстрований!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int MaxID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = "SELECT MAX(Customer_ID) FROM Customer";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
