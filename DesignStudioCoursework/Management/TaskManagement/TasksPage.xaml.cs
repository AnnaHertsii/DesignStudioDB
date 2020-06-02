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
        SearchTask search = new SearchTask();
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
            //ProjectsWindow projects = new ProjectsWindow(order);
            //projects.Show();
        }
    }
}
