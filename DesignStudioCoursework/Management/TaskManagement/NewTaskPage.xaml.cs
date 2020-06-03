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

namespace DesignStudioCoursework.Management.TaskManagement
{
    public partial class NewTaskPage : Page
    {
        private Action goBack;

        public NewTaskPage(Action goBack)
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

        private void project_Click(object sender, RoutedEventArgs e)
        {
            ProjectsWindow projects = new ProjectsWindow(project);
            projects.Show();
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            AddTask();
        }

        public void AddTask()
        {
            try
            {
                int task_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                int Status_id = statuscombo.SelectedIndex + 1;
                int Employee_id = employeecombo.SelectedIndex + 1;

                string formattedstart = null;
                string formattedend = null;
                DateTime? startDate = start_date.SelectedDate;
                DateTime? endDate = end_date.SelectedDate;
                if (startDate.HasValue && endDate.HasValue)
                {
                    formattedstart = startDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    formattedend = endDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                }

                string strSQL = string.Format("INSERT INTO [Task](Task_ID, Task_name, Description, Start_date, End_date, Employee_Ref, Project_Ref, Task_status_Ref) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    task_id, name.Text, description.Text, formattedstart, formattedend, Employee_id, getProjectID(), Status_id);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Нову задачу створено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int getProjectID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Project_ID FROM [Design Project] WHERE Project_name = '{0}'", project.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int MaxID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = "SELECT MAX(Task_ID) FROM [Task]";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
