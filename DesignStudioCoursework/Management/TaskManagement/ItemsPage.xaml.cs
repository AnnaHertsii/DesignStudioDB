using DesignStudioCoursework.Structure;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class ItemsPage : Page
    {
        Action goBack;
        DisplayItem display = new DisplayItem();
        int currentId = 0;

        public ItemsPage(Action goBack)
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

        private void ChooseTaskButton_Click(object sender, RoutedEventArgs e)
        {
            TasksWindow projects = new TasksWindow(task, task_id);
            projects.Show();
        }

        private void ShowTaskItemsButton_Click(object sender, RoutedEventArgs e)
        {
            int task_ref = 0;
            if (task_id.Text != "") task_ref = Int32.Parse(task_id.Text);
            display.ShowItemsForTask(DataGridItem, task_ref);
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem();
            int task_ref = 0;
            if (task_id.Text != "") task_ref = Int32.Parse(task_id.Text);
            display.ShowItemsForTask(DataGridItem, task_ref);
        }
    
        public void DeleteItem()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (currentId != 0)
            {
                string strSQL = string.Format("DELETE [Item] WHERE Item_ID = '{0}'", currentId);
                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Предмет видалено!");
            }
            else MessageBox.Show("Виберіть предмет перед його видаленням!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridItem.SelectedCells[index];
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
            string Amount = GetSelectedCellValue(2);
            MessageBox.Show(Name);
            MessageBox.Show(Amount);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Item_ID FROM [Item] WHERE Item_name = '{0}' AND Item_amount = {1}", Name, Int32.Parse(Amount));
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            MessageBox.Show(st);
            return Int32.Parse(st);
        }

        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateItem();
            int task_ref = 0;
            if (task_id.Text != "") task_ref = Int32.Parse(task_id.Text);
            display.ShowItemsForTask(DataGridItem, task_ref);
        }

        public void UpdateItem()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            if (currentId != 0)
            {
                string strSQL = string.Format("UPDATE Item SET Item_name = '{0}', Item_amount = '{1}' WHERE Item_ID = '{2}'", GetSelectedCellValue(0), GetSelectedCellValue(2), currentId);
                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();
                MessageBox.Show("Предмет оновлено!");
            }
            else MessageBox.Show("Виберіть предмет перед тим як його оновити!");
        }

        private void ChooseItemButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            currentId = CurrentID();
        }
    }
}
