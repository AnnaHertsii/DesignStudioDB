using DesignStudioCoursework.Structure;
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

namespace DesignStudioCoursework.Administration.NewEmployee
{
    public partial class UpdateEmployeeWindow : Window
    {
        public int employee_index;
        DataGrid datagrid;
        DisplayEmployee display = new DisplayEmployee();

        public UpdateEmployeeWindow(int index, DataGrid grid_name)
        {
            InitializeComponent();
            employee_index = index;
            datagrid = grid_name;
            fillEmployeeFields();
        }

        public void fillEmployeeFields()
        {
            using (var db = new DesignStudioEntities())
            {
                var chosenEmployee = (from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                }).FirstOrDefault(); ;
                name.Text = chosenEmployee.Name;
                //birthdate.SelectedDate = chosenEmployee.Birthdate;
                adress.Text = chosenEmployee.Adress;
                phone.Text = chosenEmployee.Phone;
                passport.Text = chosenEmployee.Passport;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /*private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
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
        }*/
    }
}
