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
                                where employee.Employee_ID == employee_index
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = employee.Position_Ref
                                }).FirstOrDefault(); 
                name.Text = chosenEmployee.Name;
                birthdate.SelectedDate = chosenEmployee.Birthdate;
                adress.Text = chosenEmployee.Adress;
                phone.Text = chosenEmployee.Phone;
                passport.Text = chosenEmployee.Passport;
                position.SelectedIndex = (int)chosenEmployee.Position - 1;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void UpdateEmployee()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            int Position_id = position.SelectedIndex + 1;
            string formatteddate = null;
            DateTime? date = birthdate.SelectedDate;            
            if (date.HasValue)
            {
                formatteddate = date.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }

            string strSQL = string.Format("Update Employee Set Employee_ID = '{0}', Name ='{1}', Birth_date = '{2}', Residence_place = '{3}', Phone = '{4}',  Passport_number = '{5}', Position_Ref = '{6}'" +
                " Where Employee_ID = '{7}'", employee_index, name.Text, formatteddate, adress.Text, phone.Text, passport.Text, Position_id, employee_index);

            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Дані працівника успішно оновлено!");
            this.Close();
        }

        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
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
 
            if ((name.Text.Length < 150) && (adress.Text.Length < 50) && (phone.Text.Length < 15) && (passport.Text.Length < 50))
            {
                name_error.Visibility = Visibility.Hidden;
                adress_error.Visibility = Visibility.Hidden;
                phone_error.Visibility = Visibility.Hidden;
                passport_error.Visibility = Visibility.Hidden;
                UpdateEmployee();
            }
            display.ShowEmployees(datagrid);
        }
    }
}
