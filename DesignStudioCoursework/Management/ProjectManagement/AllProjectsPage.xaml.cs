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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.Management.ProjectManagement
{
    public partial class AllProjectsPage : Page
    {
        private Action goBack;
        SearchProject search = new SearchProject();

        public AllProjectsPage(Action goBack)
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

       /* 
        private void UpdateOrderButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateOrder();
        }

        public void UpdateOrder()
        {
            int index = CurrentID();
            UpdateOrderWindow updateOrder = new UpdateOrderWindow(index, DataGridOrder);
            updateOrder.Show();
        }
*/

        private void ShowProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProjects();
        }

        public void ShowProjects()
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               select new
                                {
                                 Name = project.Project_name,
                                 Price = project.Project_price,
                                 Employee = employee.Name,
                                 Order = order.Description,
                                 Interior = interior.Interior_type1,
                                 Style = style.Style_name,
                                 Status = status.Status1
                                };
                DataGridProject.ItemsSource = projects.ToList();
            }
        }

        private void DeleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteProject();
            ShowProjects();
        }

        public void DeleteProject()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int SelectedId = CurrentID_Delete();
            string strSQL = string.Format("DELETE [Design Project] WHERE Project_ID = '{0}'", SelectedId);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Проект видалено!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridProject.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        public int CurrentID_Delete()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(0);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Project_ID FROM [Design Project] WHERE Project_name = '{0}' ORDER BY Project_ID DESC", Name);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(0);
            string Price = GetSelectedCellValue(1);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Project_ID FROM [Design Project] WHERE Project_name = '{0}' AND Project_price  = '{1}'", Name, Price);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        private void FindProjectButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowProjectsByOption(DataGridProject, combobox_option, search_text);
        }

        private void UpdateProjectButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
