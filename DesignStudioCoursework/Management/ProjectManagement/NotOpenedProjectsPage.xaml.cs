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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.Management.ProjectManagement
{
    public partial class NotOpenedProjectsPage : Page
    {

        private Action goBack;
        DisplayProject display = new DisplayProject();

        public NotOpenedProjectsPage(Action goBack)
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

        private void ShowNotOpenedProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            display.ShowNotOpenedProjects(DataGridProject);
        }

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Desc = GetSelectedCellValue(0);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Order_ID FROM [Order] WHERE Description = '{0}'", Desc);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
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

        private void CreateProjectButton_Click(object sender, RoutedEventArgs e)
        {
            string Desc = GetSelectedCellValue(0);
            NewProjectOrdPage newProject = new NewProjectOrdPage(Desc, goBack);
            this.NavigationService.Navigate(newProject);
        }
    }
}
