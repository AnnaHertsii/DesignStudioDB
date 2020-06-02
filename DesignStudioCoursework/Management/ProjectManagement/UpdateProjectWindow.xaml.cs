using DesignStudioCoursework.NewProject;
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

namespace DesignStudioCoursework.Management.ProjectManagement
{
    public partial class UpdateProjectWindow : Window
    {
        int project_index;
        DataGrid datagrid;
        DisplayProject display = new DisplayProject();
        public List<Style> Styles { get; set; }

        public UpdateProjectWindow(int index, DataGrid grid_name)
        {
            project_index = index;
            InitializeComponent();
            BindComboStyle();
            datagrid = grid_name;
            fillProjectFields();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void fillProjectFields()
        {
            using (var db = new DesignStudioEntities())
            {
                var chosenProject = (from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               where project.Project_ID == project_index
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Interior = project.Interior_type_Ref,
                                   Style = project.Style_Ref,
                                   Status = project.Project_status_Ref
                               }).FirstOrDefault(); ;
                name.Text = chosenProject.Name;
                price.Text = Math.Truncate((decimal)chosenProject.Price).ToString();
                employee.Text = chosenProject.Employee;
                order.Text = chosenProject.Order;
                interior.SelectedIndex = (int)chosenProject.Interior - 1;
                style.SelectedIndex = (int)chosenProject.Style - 1;
                status.SelectedIndex = (int)chosenProject.Status - 1;
            }
        }

        private void BindComboStyle()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Style.ToList();
            Styles = items;
            DataContext = Styles;
        }

        private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
        {           
            if (order.Text == "")
                order_error.Visibility = Visibility.Visible;
            else
            {
                order_error.Visibility = Visibility.Hidden;
            }

            if (employee.Text == "")
                employee_error.Visibility = Visibility.Visible;
            else
            {
                employee_error.Visibility = Visibility.Hidden;
            }

            if (name.Text.Length > 50)
                name_error.Visibility = Visibility.Visible;
            else
            {
                name_error.Visibility = Visibility.Hidden;
            }

            bool isDigit = true;
            foreach (char c in price.Text)
            {
                if (c < '0' || c > '9')
                    isDigit = false;
            }
            if (isDigit == false)
            {
                price_error.Visibility = Visibility.Visible;
            }
            else
            {
                price_error.Visibility = Visibility.Hidden;
            }

            if (style.SelectedIndex == -1)
                style_error.Visibility = Visibility.Visible;
            else
            {
                style_error.Visibility = Visibility.Hidden;
            }

            if ((interior.Text != "") && (order.Text != "") && (employee.Text != "") && (name.Text.Length < 50) && (isDigit != false) && (style.SelectedIndex != -1))
            {
                order_error.Visibility = Visibility.Hidden;
                employee_error.Visibility = Visibility.Hidden;
                name_error.Visibility = Visibility.Hidden;
                price_error.Visibility = Visibility.Hidden;
                style_error.Visibility = Visibility.Hidden;
                UpdateProject();
            }
            display.ShowProjects(datagrid);
        }

        public void UpdateProject()
        {
            try
            {
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                int Interior_id = interior.SelectedIndex + 1;
                int Style_id = style.SelectedIndex + 1;
                int Status_id = status.SelectedIndex + 1;
                int totalprice = 0;
                if (price.Text != "")
                {
                    totalprice = Int32.Parse(price.Text);
                }

                string strSQL = string.Format("UPDATE [Design Project] SET Project_ID = '{0}', Project_name = '{1}', Project_price = '{2}', Employee_Ref = '{3}', Order_Ref = '{4}',Interior_type_Ref = '{5}', Style_Ref = '{6}', Project_status_Ref = '{7}' WHERE Project_ID = '{8}'",
                    project_index, name.Text, totalprice, getEmployeeID(), getOrderID(), Interior_id, Style_id, Status_id, project_index);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Проект успішно оновлено!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow orders = new OrdersWindow(order);
            orders.Show();
        }

        private void employee_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employees = new EmployeesWindow(employee);
            employees.Show();
        }

        public int getOrderID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Order_ID FROM [Order] WHERE Description = '{0}'", order.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int getEmployeeID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Employee_ID FROM [Employee] WHERE Name = '{0}'", employee.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
