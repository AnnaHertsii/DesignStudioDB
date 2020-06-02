using DesignStudioCoursework.Structure;
using System;
using System.Collections.Generic;
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
    public partial class TasksPage : Page
    {
        DisplayTask display = new DisplayTask();
        private Action goBack;

        public TasksPage(Action goBack)
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


        private void FindTaskButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowTasksButton_Click(object sender, RoutedEventArgs e)
        {
            display.ShowTasks(DataGridTask);
        }
    }
}
