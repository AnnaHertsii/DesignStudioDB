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

namespace DesignStudioCoursework.Administration.NewEmployee
{
    public partial class EmployeesPage : Page
    {
        private Action goBack;

        public EmployeesPage(Action goBack)
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

        public void ShowEmployees()
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                DataGridEmployee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEmployees();
        }

        private void DeleteEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteEmployee();
            ShowEmployees();
        }

        public void DeleteEmployee()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int SelectedId = CurrentID();
            string strSQL = string.Format("DELETE Employee WHERE Employee_ID = '{0}'", SelectedId);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Працівника видалено з бази!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridEmployee.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(0);
            string Passportnumber = GetSelectedCellValue(4);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Employee_ID FROM Employee WHERE Name = '{0}' AND Passport_number = '{1}'", Name, Passportnumber);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        private void UpdateEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            int index = CurrentID();
            UpdateEmployeeWindow updateEmployee = new UpdateEmployeeWindow(index, DataGridEmployee);
            updateEmployee.Show();
        }
    }
}
