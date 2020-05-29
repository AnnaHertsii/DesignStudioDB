using DesignStudioCoursework.Administration.NewEmployee;
using DesignStudioCoursework.NewEmployee;
using DesignStudioCoursework.NewProject;
using DesignStudioCoursework.Review.ClientsReview;
using DesignStudioCoursework.Review.OrdersReview;
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
    public partial class MainWindow : Window
    {
        MainPage mainPageView;
        NewClientPage newClientPage;
        NewOrderPage newOrderPage;
        NewProjectPage newProjectPage;
        NewEmployeePage newEmployeePage;
        ClientsPage clientsPage;
        OrdersPage ordersPage;
        EmployeesPage employeesPage;

        public MainWindow()
        {
            InitializeComponent();
            mainPageView = new MainPage(PageChanged);
            MainFrame.Content = mainPageView;
        }
        int pageIndex = -2;

        private void PageChanged(int pageIndex)
        {
            this.pageIndex = pageIndex;
            switch (pageIndex)
            {
                case 0:
                    newClientPage = new NewClientPage(CloseFrame);
                    MainFrame.Content = newClientPage;
                    break;
                case 1:
                    newOrderPage = new NewOrderPage(CloseFrame);
                    MainFrame.Content = newOrderPage;
                    break;
                case 2:
                    newProjectPage = new NewProjectPage(CloseFrame);
                    MainFrame.Content = newProjectPage;
                    break;
                case 3:
                    newEmployeePage = new NewEmployeePage(CloseFrame);
                    MainFrame.Content = newEmployeePage;
                    break;
                case 4:
                    clientsPage = new ClientsPage(CloseFrame);                    
                    MainFrame.Content = clientsPage;                   
                    break;
                case 5:
                    ordersPage = new OrdersPage(CloseFrame);
                    MainFrame.Content = ordersPage;
                    break;
                case 6:
                    employeesPage = new EmployeesPage(CloseFrame);
                    MainFrame.Content = employeesPage;
                    break;
            }
        }

        private void CloseFrame()
        {
            pageIndex = -1;
            MainFrame.Content = mainPageView;
            MainFrame.DataContext = null;
        }

        private void MainFrame_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateFrameDataContext(sender, null);
        }

        private void UpdateFrameDataContext(object sender, NavigationEventArgs e)
        {
            var content = MainFrame.Content as FrameworkElement;
            if (content == null)
                return;
            content.DataContext = MainFrame.DataContext;
        }
    }
}

