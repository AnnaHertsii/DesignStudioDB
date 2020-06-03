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
    }
}
