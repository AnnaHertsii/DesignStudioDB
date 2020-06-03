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
    public partial class TasksWindow : Window
    {
        DisplayTask display = new DisplayTask();
        SearchTask search = new SearchTask();
        TextBox task;
        TextBox task_id;

        public TasksWindow(TextBox task1, TextBox task2)
        {
            InitializeComponent();
            this.task = task1;
            this.task_id = task2;
            display.ShowTasks(DataGridTask);
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void FindTaskButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowTasksByOption(DataGridTask, combobox_option, search_text, datebox);
        }

        private void ShowTasksButton_Click(object sender, RoutedEventArgs e)
        {
            display.ShowTasks(DataGridTask);
        }

        private void combobox_option_DropDownClosed(object sender, EventArgs e)
        {
            if (combobox_option.SelectedIndex == 0)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 1)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 2)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 3)
            {
                datebox.Visibility = System.Windows.Visibility.Visible;
            }
            else if (combobox_option.SelectedIndex == 4)
            {
                datebox.Visibility = System.Windows.Visibility.Visible;
            }
            else if (combobox_option.SelectedIndex == 5)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 6)
            {
                datebox.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void project_Click(object sender, RoutedEventArgs e)
        {
            ProjectsWindow projects = new ProjectsWindow(search_text);
            projects.Show();
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridTask.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }

        public int getEmployeeID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Employee_ID FROM [Employee] WHERE Name = '{0}'", GetSelectedCellValue(4));
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int getProjectID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Project_ID FROM [Design Project] WHERE Project_name = '{0}'", GetSelectedCellValue(5));
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
            string Description = GetSelectedCellValue(1);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Task_ID FROM [Task] WHERE Task_name = '{0}' AND Description = '{1}' AND Employee_Ref = '{2}' AND Project_Ref = '{3}'", Name, Description, getEmployeeID(), getProjectID());
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        private void ChooseTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = GetSelectedCellValue(0);
            task.Text = Name;
            task_id.Text = CurrentID().ToString();
            this.Close();
        }
    }
}
