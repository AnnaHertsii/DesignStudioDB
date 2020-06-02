using DesignStudioCoursework.About;
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

namespace DesignStudioCoursework
{
    public partial class MainPage : Page
    {
        private Action<int> pageChanged;

        public MainPage(Action<int> pageChanged)
        {
            this.pageChanged = pageChanged;
            InitializeComponent();
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewClientButton_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(0);
        }

        private void NewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(1);
        }

        private void NewProjectButton_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(2);
        }

        private void AddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(3);
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutUs = new AboutWindow();
            aboutUs.Show();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(4);
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(5);
        }

        private void EditEmployees_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(6);
        }

        private void AllProjects_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(7);
        }

        private void LifeCycleProjects_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(8);
        }

        private void NotOpenedProjects_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(9);
        }

        private void AllTasks_Click(object sender, RoutedEventArgs e)
        {
            pageChanged(10);
        }
    }
}
