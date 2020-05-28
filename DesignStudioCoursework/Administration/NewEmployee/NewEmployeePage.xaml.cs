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

namespace DesignStudioCoursework.NewEmployee
{
    public partial class NewEmployeePage : Page
    {
        private Action goBack;

        public NewEmployeePage(Action goBack)
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

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            String employee = selectedItem.Content.ToString();

            BitmapImage image = null;
            if (employee == "Проектувальник")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/projecter.png", UriKind.Relative));
            }
            else if (employee == "Художник")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/painter.png", UriKind.Relative));
            }
            else if (employee == "Дизайнер")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/designer.png", UriKind.Relative));
            }
            else if (employee == "Менеджер")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/manager.png", UriKind.Relative));
            }
            else if (employee == "Секретар")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/secreter.png", UriKind.Relative));
            }
            employeeType.Source = image;
        }

        private void AddEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text.Length > 150)
                name_error.Visibility = Visibility.Visible;
            else
            {
                name_error.Visibility = Visibility.Hidden;
            }
            if (adress.Text.Length > 50)
                adress_error.Visibility = Visibility.Visible;
            else
            {
                adress_error.Visibility = Visibility.Hidden;
            }
            if (phone.Text.Length > 15)
                phone_error.Visibility = Visibility.Visible;
            else
            {
                phone_error.Visibility = Visibility.Hidden;
            }
            
            if (passport.Text.Length > 50)
                passport_error.Visibility = Visibility.Visible;
            else
            {
                passport_error.Visibility = Visibility.Hidden;
            }
            if (position.SelectedIndex == -1)
                position_error.Visibility = Visibility.Visible;
            else
            {
                position_error.Visibility = Visibility.Hidden;
            }

            if ((name.Text.Length < 150) && (phone.Text.Length < 15) && (adress.Text.Length < 50) && (passport.Text.Length < 50) && (position.SelectedIndex != -1))
            {
                name_error.Visibility = Visibility.Hidden;
                adress_error.Visibility = Visibility.Hidden;
                phone_error.Visibility = Visibility.Hidden;  
                passport_error.Visibility = Visibility.Hidden;
                position_error.Visibility = Visibility.Hidden;
                AddEmployee();
            }
        }

        public void AddEmployee()
        {
            try
            {
                int employee_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();

                string formatteddate = null;
                DateTime? date = birthdate.SelectedDate;
                if (date.HasValue)
                {
                    formatteddate = date.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);                
                }
                int Position_id = position.SelectedIndex + 1;

                string strSQL = string.Format("INSERT INTO Employee(Employee_ID, Name, Birth_date, Residence_place, Phone, Passport_number, Position_Ref)" +
                    "VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", employee_id, name.Text, formatteddate, adress.Text, phone.Text, passport.Text, Position_id);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Новий працівник успішно зареєстрований!");
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
            string strSQL = "SELECT MAX(Employee_ID) FROM Employee";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
