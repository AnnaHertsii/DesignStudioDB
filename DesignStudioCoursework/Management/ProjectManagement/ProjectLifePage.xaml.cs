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
    public partial class ProjectLifePage : Page
    {
        private Action goBack;
        ProjectLifeCycle life = new ProjectLifeCycle();

        public ProjectLifePage(Action goBack)
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
       
        private void ShowProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            life.ShowProjectsByOption(DataGridProject, combobox_option);
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeStatus();
            life.ShowProjectsByOption(DataGridProject, combobox_option);
        }

        private void ChangeStatus()
        {
            int project_index = CurrentID();
            try
            {
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                int Status_id = combobox_status.SelectedIndex + 1;

                string strSQL = string.Format("UPDATE [Design Project] SET Project_status_Ref = '{0}' WHERE Project_ID = '{1}'", Status_id, project_index);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                //MessageBox.Show("Статус проекта успішно оновлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        public int CurrentID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string Name = GetSelectedCellValue(2);
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT Project_ID FROM [Design Project] WHERE Project_name = '{0}'", Name);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
