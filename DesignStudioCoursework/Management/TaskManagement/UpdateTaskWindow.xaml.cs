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
    public partial class UpdateTaskWindow : Window
    {
        public int task_index;
        DataGrid datagrid;
        DisplayTask display = new DisplayTask();

        public UpdateTaskWindow(int index, DataGrid grid_name)
        {
            task_index = index;
            InitializeComponent();
            datagrid = grid_name;
            fillTaskFields();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateTaskButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateTask();
            display.ShowTasks(datagrid);
        }

         public void UpdateTask()
         {
            try
            {
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

             string strSQL = string.Format("UPDATE [Task] SET Task_name = '{0}', Description = '{1}', Start_date = '{2}', End_date = '{3}', Employee_Ref = '{4}', Project_Ref = '{5}', Task_status_Ref = '{6}' WHERE Task_ID = '{7}'",
                 name.Text, description.Text, formattedstart, formattedend, Employee_id, getProjectID(), Status_id, task_index);

             SqlCommand myCommand = new SqlCommand(strSQL, connection);
             myCommand.ExecuteNonQuery();

             MessageBox.Show("Задачу успішно оновлено!");
             this.Close();
         }
         catch (Exception ex)
         {
             MessageBox.Show(ex.ToString());
         }
 }

        public void fillTaskFields()
        {
            using (var db = new DesignStudioEntities())
            {
                var chosenTask = (from task in db.Task
                                  join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                                  where task.Task_ID == task_index
                            select new
                            {
                                Name = task.Task_name,
                                task.Description,
                                Start = task.Start_date,
                                End = task.End_date,
                                Employee = task.Employee_Ref,
                                Project = project.Project_name,
                                Status = task.Task_status_Ref
                            }).FirstOrDefault();
                name.Text = chosenTask.Name;
                description.Text = chosenTask.Description;
                start_date.SelectedDate = chosenTask.Start;
                end_date.SelectedDate = chosenTask.End;               
                employeecombo.SelectedIndex = (int)chosenTask.Employee - 1;
                project.Text = chosenTask.Project;
                statuscombo.SelectedIndex = (int)chosenTask.Status - 1;
            }              
        }

        private void project_Click(object sender, RoutedEventArgs e)
        {
            ProjectsWindow projects = new ProjectsWindow(project);
            projects.Show();
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
    }
}
