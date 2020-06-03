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

namespace DesignStudioCoursework.Management.TaskManagement
{
    public partial class ProjectsWindow : Window
    {
        private Action goBack;
        SearchProject search = new SearchProject();
        TextBox project;

        public ProjectsWindow(TextBox project1)
        {
            InitializeComponent();
            ShowProjects();
            project = project1;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        public void ShowProjects()
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                DataGridProject.ItemsSource = projects.ToList();
            }
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

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(0);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Project_ID FROM [Design Project] WHERE Project_name = '{0}'", Name);
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

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            ShowProjects();
        }

        private void ChooseProjectButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = GetSelectedCellValue(0);
            project.Text = Name;
            this.Close();
        }
    }
}
